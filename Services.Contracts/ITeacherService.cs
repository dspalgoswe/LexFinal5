using Domain.Models.Entities;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ITeacherService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<IEnumerable<ModuleDto>> GetModulesByCourseIdAsync(int courseId);
        Task<IEnumerable<ActivityDto>> GetActivitiesByModuleIdAsync(int moduleId);
        Task<IdentityResult> CreateUserAsync(CreateUserDto userDto);
        Task<IdentityResult> UpdateUserAsync(string userId, UpdateUserDto userDto);
        Task<CourseDto> CreateCourseAsync(CreateCourseDto courseDto);
        Task<bool> UpdateCourseAsync(int courseId, UpdateCourseDto courseDto);
        Task<bool> DeleteCourseAsync(int courseId);
        Task<ModuleDto> CreateModuleAsync(CreateModuleDto moduleDto);
        Task<bool> UpdateModuleAsync(int moduleId, UpdateModuleDto moduleDto);
        Task<bool> DeleteModuleAsync(int moduleId);
        Task<ActivityDto> CreateActivityAsync(CreateActivityDto activityDto);
        Task<bool> UpdateActivityAsync(int activityId, UpdateActivityDto activityDto);
        Task<bool> DeleteActivityAsync(int activityId);
    }
}
