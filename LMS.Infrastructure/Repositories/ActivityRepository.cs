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
    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
    {
        public ActivityRepository(LmsContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Activity>> GetActivitiesByModuleIdAsync(int moduleId)
        {
            return await FindByCondition(a => a.ModuleId == moduleId)
                .Include(a => a.ActivityType)
                .Include(a => a.Documents)
                .ToListAsync();
        }

        public async Task<Activity> GetActivityByIdWithDetailsAsync(int activityId)
        {
            return await FindByCondition(a => a.ActivityId == activityId)
                .Include(a => a.ActivityType)
                .Include(a => a.Documents)
                .FirstOrDefaultAsync();
        }
    }
}