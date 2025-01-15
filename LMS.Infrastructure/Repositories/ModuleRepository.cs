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
    public class ModuleRepository : RepositoryBase<Module>, IModuleRepository
    {
        public ModuleRepository(LmsContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId)
        {
            return await FindByCondition(m => m.CourseId == courseId)
                .Include(m => m.Activities)
                .Include(m => m.Documents)
                .ToListAsync();
        }

        public async Task<Module> GetModuleByIdWithDetailsAsync(int moduleId)
        {
            return await FindByCondition(m => m.ModuleId == moduleId)
                .Include(m => m.Activities)
                .Include(m => m.Documents)
                .FirstOrDefaultAsync();
        }
    }
}
