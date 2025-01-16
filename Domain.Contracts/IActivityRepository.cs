using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IActivityRepository : IRepositoryBase<Activity>
    {
        Task<Activity?> GetActivityByIdAsync(int activityId, bool trackChanges = false);
        Task<IEnumerable<Activity>> GetActivitiesByModuleIdAsync(int moduleId, bool trackChanges = false);
        Task<Activity> GetActivityByIdWithDetailsAsync(int activityId);
    }
}
