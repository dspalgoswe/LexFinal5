using LMS.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IStudentService
    {
        //Hämtar studenten
        Task<UserDto> GetStudentByIdAsync(int studentId);

        //Hämtar studentens nuvarande kurs
        Task<CourseDto> GetStudentCourseAsync(string studentId);

        //Hämtar deltagare i en viss kurs
        Task<List<UserDto>> GetCourseParticipantsAsync(int courseId);

        //Hämtar modulerna i studentens kurs
        Task<List<ModuleDto>> GetModulesByCourseIdAsync(int courseId);

        //Hämtar aktiviteter för en viss modul
        Task<List<ActivityDto>> GetActivitiesByModuleIdAsync(int moduleId);

        //Hämtar detaljer för en specifik aktivitet
        Task<ActivityDto> GetActivityByIdAsync(int activityId);

        //Uppdaterar studentens profilinformation
        Task<bool> UpdateStudentProfileAsync(int studentId, UpdateUserDto studentDto);

    }
}
