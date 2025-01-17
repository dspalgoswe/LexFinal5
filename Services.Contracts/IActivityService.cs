using LMS.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityDto>> GetActivitiesByModuleIdAsync(int moduleId);
        Task<ActivityDto> GetActivityAsync(int activityId);
        Task<ActivityDto> CreateActivityAsync(ActivityDto activityDto);
        Task<bool> UpdateActivityAsync(int activityId, ActivityDto activityDto);
        Task<bool> DeleteActivityAsync(int activityId);
    }
}
