using AutoMapper;
using Domain.Models.Entities;
using LMS.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly LmsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ILogger<TeacherService> _logger;

        public CourseService(
            IMapper mapper,
            LmsContext context,
            UserManager<ApplicationUser> userManager)
            //ILogger<TeacherService> logger)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            try
            {
                return await _context.Courses
                    .Include(c => c.Modules)
                        .ThenInclude(m => m.Activities)
                            .ThenInclude(a => a.ActivityType)
                    .Include(c => c.Users)
                    .Include(c => c.Documents)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred while getting all courses");
                throw;
            }
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            try
            {
                var course = await _context.Courses
                    .Include(c => c.Modules)
                        .ThenInclude(m => m.Activities)
                            .ThenInclude(a => a.ActivityType)
                    .Include(c => c.Users)
                    .Include(c => c.Documents)
                    .FirstOrDefaultAsync(c => c.CourseId == id);

                if (course == null)
                {
                    //_logger.LogWarning("Course with ID {CourseId} was not found", id);
                    throw new KeyNotFoundException($"Course with ID {id} not found");
                }

                return course;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred while getting course with ID {CourseId}", id);
                throw;
            }
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            try
            {
                await _context.Courses.AddAsync(course);
                await _context.SaveChangesAsync();

                //_logger.LogInformation("Created new course with ID {CourseId}", course.CourseId);
                return course;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred while creating new course");
                throw;
            }
        }

        public async Task<Course> UpdateCourseAsync(int id, Course updatedCourse)
        {
            try
            {
                var existingCourse = await _context.Courses
                    .Include(c => c.Users)
                    .FirstOrDefaultAsync(c => c.CourseId == id);

                if (existingCourse == null)
                {
                    //_logger.LogWarning("Course with ID {CourseId} was not found for update", id);
                    throw new KeyNotFoundException($"Course with ID {id} not found");
                }

                // Update properties
                existingCourse.Name = updatedCourse.Name;
                existingCourse.Description = updatedCourse.Description;
                existingCourse.StartDate = updatedCourse.StartDate;

                await _context.SaveChangesAsync();
                //_logger.LogInformation("Updated course with ID {CourseId}", id);

                return existingCourse;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred while updating course with ID {CourseId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            try
            {
                var course = await _context.Courses.FindAsync(id);
                if (course == null)
                {
                    //_logger.LogWarning("Course with ID {CourseId} was not found for deletion", id);
                    return false;
                }

                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();

                //_logger.LogInformation("Deleted course with ID {CourseId}", id);
                return true;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred while deleting course with ID {CourseId}", id);
                throw;
            }
        }

        public async Task<bool> AddUserToCourseAsync(int courseId, string userId)
        {
            try
            {
                var course = await _context.Courses
                    .Include(c => c.Users)
                    .FirstOrDefaultAsync(c => c.CourseId == courseId);

                if (course == null)
                {
                    //_logger.LogWarning("Course with ID {CourseId} was not found when adding user", courseId);
                    return false;
                }

                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    //_logger.LogWarning("User with ID {UserId} was not found when adding to course", userId);
                    return false;
                }

                if (course.Users == null)
                {
                    course.Users = new List<ApplicationUser>();
                }

                if (!course.Users.Any(u => u.Id == userId))
                {
                    course.Users.Add(user);
                    await _context.SaveChangesAsync();
                    //_logger.LogInformation("Added user {UserId} to course {CourseId}", userId, courseId);
                }

                return true;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred while adding user {UserId} to course {CourseId}", userId, courseId);
                throw;
            }
        }

        public async Task<bool> RemoveUserFromCourseAsync(int courseId, string userId)
        {
            try
            {
                var course = await _context.Courses
                    .Include(c => c.Users)
                    .FirstOrDefaultAsync(c => c.CourseId == courseId);

                if (course == null || course.Users == null)
                {
                    return false;
                }

                var user = course.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return false;
                }

                course.Users.Remove(user);
                await _context.SaveChangesAsync();

                //_logger.LogInformation("Removed user {UserId} from course {CourseId}", userId, courseId);
                return true;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred while removing user {UserId} from course {CourseId}", userId, courseId);
                throw;
            }
        }

        public async Task<IEnumerable<Course>> GetCoursesByUserIdAsync(string userId)
        {
            try
            {
                return await _context.Courses
                    .Include(c => c.Modules)
                    .Include(c => c.Users)
                    .Where(c => c.Users.Any(u => u.Id == userId))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred while getting courses for user {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId)
        {
            try
            {
                var course = await _context.Courses
                    .Include(c => c.Modules)
                        .ThenInclude(m => m.Activities)
                    .FirstOrDefaultAsync(c => c.CourseId == courseId);

                return course?.Modules ?? new List<Module>();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred while getting modules for course {CourseId}", courseId);
                throw;
            }
        }

        public async Task<bool> CourseExistsAsync(int id)
        {
            return await _context.Courses.AnyAsync(c => c.CourseId == id);
        }
    }
}
