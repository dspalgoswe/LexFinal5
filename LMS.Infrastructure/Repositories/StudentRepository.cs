using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repositories
{
    public class StudentRepository : RepositoryBase<ApplicationUser>, IStudentRepository
    {
        private readonly LmsContext _context;
        public StudentRepository(LmsContext context) : base(context)
        {
            _context = context;
        }

        //Hämtar Alla studenter
        public async Task<List<ApplicationUser>> GetAllStudentsAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
            //.Include(u => u.Course) //Inkludera kursinformation

        }

        //Hämtar en specefik student med stundetId
        public async Task<ApplicationUser> GetStudentByIdAsync(int studentId)
        {
            return await FindByCondition(u => u.Id == studentId.ToString(), trackChanges: false)
            .Include(u => u.Course) //Inkluderar kursrelationen
            .FirstOrDefaultAsync();
        }

        //Hämtar studentens kurs
        public async Task<ApplicationUser> GetStudentCourseAsync(string studentId)
        {
           var student = await _context.Users
                .Where(u => u.Id == studentId)
                .Include(u => u.Course) //Hämtar kursrelationen
                .FirstOrDefaultAsync();

            return student;

        }
        
        public async Task<List<ApplicationUser>> GetCourseParticipantsAsync(int courseId)
        {
            return await _context.Users
            .Where(u => u.CourseId == courseId)
            .ToListAsync();
        }

        //Hämtar Alla moduler för en viss kurs
        public async Task<List<Module>> GetModulesByCourseIdAsync(int courseId)
        {
            return await _context.Modules
             .Where(m => m.CourseId == courseId)
             .Include(m => m.Activities) //Inkluderar aktiviteter i varje modul
             .ToListAsync();
        }

        public async Task<List<Activity>> GetActivitiesByModuleIdAsync(int moduleId)
        {
            return await _context.Activities
            .Where(a => a.ModuleId == moduleId)
            .Include(a => a.ActivityType) //Inkluderar aktivitetstyp
            .ToListAsync();
        }

        //Hämtar en specefik activity via Id
        public async Task<Activity> GetActivityByIdAsync(int activityId)
        {
            return await _context.Activities
                .Where(a => a.ActivityId == activityId)
                .Include(a => a.ActivityType) //Inkluderar aktivitetstyp
                .FirstOrDefaultAsync();              
            
        }

        //public void Delete(ApplicationUser entity)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<bool> DeleteStudentAsync(string studentId)
        {
            var studentToDelete = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == studentId);

            if (studentToDelete == null)
            {
                return false; //Studenten hittades inte
            }

            _context.Users.Remove(studentToDelete);

            return await _context.SaveChangesAsync() > 0; //Returnerar true om raderingen lyckades
        }



        //public void Update(ApplicationUser student)
        //{
        //    _context.Users.Update(student);
        //}

        public async Task<bool> UpdateStudentProfileAsync(string studentId, ApplicationUser student)
        {
            var existingStudent = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == studentId);

            if (existingStudent == null)
            {
                return false; //Studenten hittades inte
            }

            //Uppdaterar fält
            existingStudent.UserName = student.UserName;
            existingStudent.Email = student.Email;

            //Lägger till fler fält om det behövs
            _context.Users.Update(existingStudent);

            return await _context.SaveChangesAsync() > 0; //Returnerar true om ändringar sparades
        }

    }
}
