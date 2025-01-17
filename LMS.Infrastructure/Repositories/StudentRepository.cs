using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repositories
{
    public class StudentRepository : RepositoryBase<Course>, IStudentRepository
    {
        public StudentRepository(LmsContext context) : base(context)
        {
        }

        public void Create(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteStudentAsync(string studentId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ApplicationUser> FindByCondition(Expression<Func<ApplicationUser, bool>> expression, bool trackChanges = false)
        {
            throw new NotImplementedException();
        }

        public Task<List<Activity>> GetActivitiesByModuleIdAsync(int moduleId)
        {
            throw new NotImplementedException();
        }

        public Task<Activity> GetActivityByIdAsync(int activityId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Course>> GetAllCoursesAsync(bool trackChanges = false)
        {
            return await FindAll(trackChanges)
                .Include(c => c.Modules)
                .ThenInclude(m => m.Activities)
                .ToListAsync();
        }

        public Task<List<ApplicationUser>> GetAllStudentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ApplicationUser>> GetCourseParticipantsAsync(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Module>> GetModulesByCourseIdAsync(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetStudentByIdAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetStudentCourseAsync(string studentId)
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStudentProfileAsync(string studentId, ApplicationUser student)
        {
            throw new NotImplementedException();
        }

        IQueryable<ApplicationUser> IRepositoryBase<ApplicationUser>.FindAll(bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
