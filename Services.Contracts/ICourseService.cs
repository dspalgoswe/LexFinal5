using Domain.Models.Entities;
using LMS.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(int id);
        Task<CourseDto> CreateCourseAsync(CourseDto courseDto);
        Task<Course> UpdateCourseAsync(int id, CourseDto courseDto);
        Task<bool> DeleteCourseAsync(int id);
        Task<bool> AddUserToCourseAsync(int courseId, string userId);
        Task<bool> RemoveUserFromCourseAsync(int courseId, string userId);
        Task<IEnumerable<Course>> GetCoursesByUserIdAsync(string userId);
        Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId);
    }
}
