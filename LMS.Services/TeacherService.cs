//using AutoMapper;
//using Domain.Contracts;
//using Domain.Models.Entities;
//using LMS.Shared.DTOs;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Services.Contracts;

//namespace LMS.Services
//{
//    public class TeacherService : ITeacherService
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;
//        private readonly UserManager<ApplicationUser> _userManager;

//        public TeacherService(
//            IUnitOfWork unitOfWork,
//            IMapper mapper,
//            UserManager<ApplicationUser> userManager)
//        {
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//            _userManager = userManager;
//        }

//        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
//        {
//            var courses = await _unitOfWork.Course.GetAllWithDetailsAsync();
//            return _mapper.Map<IEnumerable<CourseDto>>(courses);
//        }

//        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto courseDto)
//        {
//            if (courseDto == null) throw new ArgumentNullException(nameof(courseDto));

//            var users = await _userManager.Users
//                .Where(u => courseDto.UserIds.Contains(u.Id))
//                .ToListAsync();

//            if (users.Count != courseDto.UserIds.Count)
//                throw new KeyNotFoundException("One or more users were not found.");

//            var course = _mapper.Map<Course>(courseDto);
//            course.Users = users;

//            _unitOfWork.Course.Create(course);
//            await _unitOfWork.CompleteASync();

//            return _mapper.Map<CourseDto>(course);
//        }

//        public async Task<IEnumerable<ModuleDto>> GetModulesByCourseIdAsync(int courseId)
//        {
//            var modules = await _unitOfWork.Module.GetModulesByCourseIdAsync(courseId);
//            return _mapper.Map<IEnumerable<ModuleDto>>(modules);
//        }

//        public async Task<IEnumerable<ActivityDto>> GetActivitiesByModuleIdAsync(int moduleId)
//        {
//            var activities = await _unitOfWork.Activity.GetActivitiesByModuleIdAsync(moduleId);
//            return _mapper.Map<IEnumerable<ActivityDto>>(activities);
//        }

//        public async Task<ModuleDto> CreateModuleAsync(CreateModuleDto moduleDto)
//        {
//            var module = _mapper.Map<Module>(moduleDto);
//            _unitOfWork.Module.Create(module);
//            await _unitOfWork.CompleteASync();
//            return _mapper.Map<ModuleDto>(module);
//        }

//        public async Task<bool> UpdateModuleAsync(int moduleId, UpdateModuleDto moduleDto)
//        {
//            var module = await _unitOfWork.Module
//                .FindByCondition(m => m.ModuleId == moduleId)
//                .FirstOrDefaultAsync();

//            if (module == null) return false;

//            _mapper.Map(moduleDto, module);
//            _unitOfWork.Module.Update(module);
//            await _unitOfWork.CompleteASync();
//            return true;
//        }

//        public async Task<bool> DeleteModuleAsync(int moduleId)
//        {
//            var module = await _unitOfWork.Module
//                .FindByCondition(m => m.ModuleId == moduleId)
//                .FirstOrDefaultAsync();

//            if (module == null) return false;

//            _unitOfWork.Module.Delete(module);
//            await _unitOfWork.CompleteASync();
//            return true;
//        }

//        public async Task<ActivityDto> CreateActivityAsync(CreateActivityDto activityDto)
//        {
//            var activity = _mapper.Map<Activity>(activityDto);
//            _unitOfWork.Activity.Create(activity);
//            await _unitOfWork.CompleteASync();

//            var createdActivity = await _unitOfWork.Activity
//                .GetActivityByIdWithDetailsAsync(activity.ActivityId);
//            return _mapper.Map<ActivityDto>(createdActivity);
//        }

//        public async Task<bool> UpdateActivityAsync(int activityId, UpdateActivityDto activityDto)
//        {
//            var activity = await _unitOfWork.Activity
//                .FindByCondition(a => a.ActivityId == activityId)
//                .FirstOrDefaultAsync();

//            if (activity == null) return false;

//            _mapper.Map(activityDto, activity);
//            _unitOfWork.Activity.Update(activity);
//            await _unitOfWork.CompleteASync();
//            return true;
//        }

//        public async Task<bool> DeleteActivityAsync(int activityId)
//        {
//            var activity = await _unitOfWork.Activity
//                .FindByCondition(a => a.ActivityId == activityId)
//                .FirstOrDefaultAsync();

//            if (activity == null) return false;

//            _unitOfWork.Activity.Delete(activity);
//            await _unitOfWork.CompleteASync();
//            return true;
//        }

//        public Task<IdentityResult> CreateUserAsync(CreateUserDto userDto)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IdentityResult> UpdateUserAsync(string userId, UpdateUserDto userDto)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> UpdateCourseAsync(int courseId, UpdateCourseDto courseDto)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> DeleteCourseAsync(int courseId)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}