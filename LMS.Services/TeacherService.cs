using AutoMapper;
using Domain.Models.Entities;
using LMS.Infrastructure.Data;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly LmsContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<TeacherService> _logger;

        public TeacherService(
            LmsContext context,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            ILogger<TeacherService> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _context.Courses
                .Include(c => c.Modules)
                .Include(c => c.Users)
                .Include(c => c.Documents)
                .ToListAsync();

            return courses.Select(c => new CourseDto
            {
                CourseId = c.CourseId,
                Name = c.Name,
                Description = c.Description,
                StartDate = c.StartDate,
                Users = c.Users.Select(u => new UserDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    // Map other user properties
                }).ToList(),
                Modules = c.Modules.Select(m => new ModuleDto
                {
                    ModuleId = m.ModuleId,
                    Name = m.Name,
                    Description = m.Description,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    CourseId = m.CourseId
                }).ToList(),
                Documents = c.Documents.Select(d => new DocumentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Type = d.Type,
                    CourseId = d.CourseId
                }).ToList()
            });
        }

        public async Task<IEnumerable<ModuleDto>> GetModulesByCourseIdAsync(int courseId)
        {
            var modules = await _context.Modules
                .Where(m => m.CourseId == courseId)
                .Include(m => m.Activities)
                .Include(m => m.Documents)
                .ToListAsync();

            return modules.Select(m => new ModuleDto
            {
                ModuleId = m.ModuleId,
                Name = m.Name,
                Description = m.Description,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                CourseId = m.CourseId,
                Activities = m.Activities.Select(a => new ActivityDto
                {
                    ActivityId = a.ActivityId,
                    Name = a.Name,
                    Description = a.Description,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    ModuleId = a.ModuleId,
                    ActivityType = new ActivityTypeDto
                    {
                        ActivityTypeId = a.ActivityTypeId,
                        Type = a.ActivityType.Type,
                        Deadlines = a.ActivityType.Deadlines
                    }
                }).ToList(),
                Documents = m.Documents.Select(d => new DocumentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Type = d.Type,
                    ModuleId = d.ModuleId
                }).ToList()
            });
        }

        public async Task<IEnumerable<ActivityDto>> GetActivitiesByModuleIdAsync(int moduleId)
        {
            var activities = await _context.Activities
                .Where(a => a.ModuleId == moduleId)
                .Include(a => a.ActivityType)
                .Include(a => a.Documents)
                .ToListAsync();

            return activities.Select(a => new ActivityDto
            {
                ActivityId = a.ActivityId,
                Name = a.Name,
                Description = a.Description,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                ModuleId = a.ModuleId,
                ActivityType = new ActivityTypeDto
                {
                    ActivityTypeId = a.ActivityTypeId,
                    Type = a.ActivityType.Type,
                    Deadlines = a.ActivityType.Deadlines
                },
                Documents = a.Documents.Select(d => new DocumentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Type = d.Type,
                    ActivityId = d.ActivityId
                }).ToList()
            });
        }
        public async Task<IdentityResult> CreateUserAsync(CreateUserDto userDto)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = userDto.Email,
                    Email = userDto.Email
                };

                var result = await _userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, userDto.Role);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user with email {Email}", userDto.Email);
                throw;
            }
        }

        public async Task<IdentityResult> UpdateUserAsync(string userId, UpdateUserDto userDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return IdentityResult.Failed(new IdentityError
                    {
                        Description = "User not found"
                    });
                }

                user.Email = userDto.Email;
                user.UserName = userDto.Email;

                return await _userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user {UserId}", userId);
                throw;
            }
        }

        // Course Management Methods
        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto courseDto)
        {
            // Validate courseDto
            if (courseDto == null)
                throw new ArgumentNullException(nameof(courseDto));

            // Check if the users exist
            var users = await _userManager.Users
                .Where(u => courseDto.UserIds.Contains(u.Id))
                .ToListAsync();

            if (users.Count != courseDto.UserIds.Count)
                throw new KeyNotFoundException("One or more users were not found.");

            // Map DTO to Course entity
            var course = _mapper.Map<Course>(courseDto);
            course.Users = users; // Assign the users to the course

            // Save the course to the database
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            // Map the created course to CourseDto and return
            return _mapper.Map<CourseDto>(course);
        }


        public async Task<bool> UpdateCourseAsync(int courseId, UpdateCourseDto courseDto)
        {
            try
            {
                var course = await _context.Courses
                    .Include(c => c.Users)
                    .FirstOrDefaultAsync(c => c.CourseId == courseId);

                if (course == null)
                    return false;

                course.Name = courseDto.Name;
                course.Description = courseDto.Description;
                course.StartDate = courseDto.StartDate;

                // Update user list if provided
                if (courseDto.UserIds != null)
                {
                    course.Users.Clear();
                    foreach (var userId in courseDto.UserIds)
                    {
                        var user = await _userManager.FindByIdAsync(userId);
                        if (user != null)
                        {
                            course.Users.Add(user);
                        }
                    }
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating course {CourseId}", courseId);
                throw;
            }
        }

        public async Task<bool> DeleteCourseAsync(int courseId)
        {
            try
            {
                var course = await _context.Courses.FindAsync(courseId);
                if (course == null)
                    return false;

                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting course {CourseId}", courseId);
                throw;
            }
        }
        public async Task<ModuleDto> CreateModuleAsync(CreateModuleDto moduleDto)
        {
            try
            {
                var module = new Module
                {
                    Name = moduleDto.Name,
                    Description = moduleDto.Description,
                    StartDate = moduleDto.StartDate,
                    EndDate = moduleDto.EndDate,
                    CourseId = moduleDto.CourseId
                };

                _context.Modules.Add(module);
                await _context.SaveChangesAsync();

                return new ModuleDto
                {
                    ModuleId = module.ModuleId,
                    Name = module.Name,
                    Description = module.Description,
                    StartDate = module.StartDate,
                    EndDate = module.EndDate,
                    CourseId = module.CourseId,
                    Activities = new List<ActivityDto>(),
                    Documents = new List<DocumentDto>()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating module {ModuleName}", moduleDto.Name);
                throw;
            }
        }

        public async Task<bool> UpdateModuleAsync(int moduleId, UpdateModuleDto moduleDto)
        {
            try
            {
                var module = await _context.Modules.FindAsync(moduleId);
                if (module == null)
                    return false;

                module.Name = moduleDto.Name;
                module.Description = moduleDto.Description;
                module.StartDate = moduleDto.StartDate;
                module.EndDate = moduleDto.EndDate;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating module {ModuleId}", moduleId);
                throw;
            }
        }

        public async Task<bool> DeleteModuleAsync(int moduleId)
        {
            try
            {
                var module = await _context.Modules.FindAsync(moduleId);
                if (module == null)
                    return false;

                _context.Modules.Remove(module);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting module {ModuleId}", moduleId);
                throw;
            }
        }

        // Activity Management Methods
        public async Task<ActivityDto> CreateActivityAsync(CreateActivityDto activityDto)
        {
            try
            {
                var activity = new Activity
                {
                    Name = activityDto.Name,
                    Description = activityDto.Description,
                    StartDate = activityDto.StartDate,
                    EndDate = activityDto.EndDate,
                    ModuleId = activityDto.ModuleId,
                    ActivityTypeId = activityDto.ActivityTypeId
                };

                _context.Activities.Add(activity);
                await _context.SaveChangesAsync();

                var activityType = await _context.ActivityTypes.FindAsync(activity.ActivityTypeId);

                return new ActivityDto
                {
                    ActivityId = activity.ActivityId,
                    Name = activity.Name,
                    Description = activity.Description,
                    StartDate = activity.StartDate,
                    EndDate = activity.EndDate,
                    ModuleId = activity.ModuleId,
                    ActivityType = new ActivityTypeDto
                    {
                        ActivityTypeId = activityType.ActivityTypeId,
                        Type = activityType.Type,
                        Deadlines = activityType.Deadlines
                    },
                    Documents = new List<DocumentDto>()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating activity {ActivityName}", activityDto.Name);
                throw;
            }
        }

        public async Task<bool> UpdateActivityAsync(int activityId, UpdateActivityDto activityDto)
        {
            try
            {
                var activity = await _context.Activities.FindAsync(activityId);
                if (activity == null)
                    return false;

                activity.Name = activityDto.Name;
                activity.Description = activityDto.Description;
                activity.StartDate = activityDto.StartDate;
                activity.EndDate = activityDto.EndDate;
                activity.ActivityTypeId = activityDto.ActivityTypeId;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating activity {ActivityId}", activityId);
                throw;
            }
        }

        public async Task<bool> DeleteActivityAsync(int activityId)
        {
            try
            {
                var activity = await _context.Activities.FindAsync(activityId);
                if (activity == null)
                    return false;

                _context.Activities.Remove(activity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting activity {ActivityId}", activityId);
                throw;
            }
        }
    }
}