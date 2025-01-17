using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IStudentRepository : IRepositoryBase<ApplicationUser>
    {
        Task<List<ApplicationUser>> GetAllStudentsAsync();
        Task<ApplicationUser> GetStudentByIdAsync(int studentId);
        Task<ApplicationUser> GetStudentCourseAsync(string studentId);
        Task<List<ApplicationUser>> GetCourseParticipantsAsync(int courseId);
        Task<List<Module>> GetModulesByCourseIdAsync(int courseId);
        Task<List<Activity>> GetActivitiesByModuleIdAsync(int moduleId);
        Task<Activity> GetActivityByIdAsync(int activityId);
        Task<bool> UpdateStudentProfileAsync(string studentId, ApplicationUser student);
        Task<bool> DeleteStudentAsync(string studentId);

    }
}
