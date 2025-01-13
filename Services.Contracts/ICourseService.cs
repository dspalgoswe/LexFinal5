using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task<Course> CreateCourseAsync(Course course);
        Task<Course> UpdateCourseAsync(int id, Course course);
        Task<bool> DeleteCourseAsync(int id);
        Task<bool> AddUserToCourseAsync(int courseId, string userId);
        Task<bool> RemoveUserFromCourseAsync(int courseId, string userId);
        Task<IEnumerable<Course>> GetCoursesByUserIdAsync(string userId);
        Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId);
        Task<bool> CourseExistsAsync(int id);
    }
}
