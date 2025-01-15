using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repositories
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(LmsContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Course>> GetAllCoursesWithDetailsAsync()
        {
            return await FindAll()
                .Include(c => c.Modules)
                .Include(c => c.Users)
                .Include(c => c.Documents)
                .ToListAsync();
        }
        public async Task<Course> GetCourseByIdWithDetailsAsync(int courseId)
        {
            return await FindByCondition(c => c.CourseId == courseId)
                .Include(c => c.Modules)
                .Include(c => c.Users)
                .Include(c => c.Documents)
                .FirstOrDefaultAsync();
        }

        public Task<IEnumerable<Course>> GetAllWithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Course> GetByIdWithDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
