using AutoMapper;
using LMS.Infrastructure.Data;
using LMS.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Contracts;

namespace LMS.Services
{
    public class ActivityService : IActivityService
    {
        private readonly LmsContext _context;
        private readonly IMapper _mapper;

        public ActivityService(LmsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActivityDto>> GetActivitiesByModuleIdAsync(int moduleId)
        {
            var activities = await _context.Activities
                .Where(a => a.ModuleId == moduleId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ActivityDto>>(activities);
        }

        public async Task<ActivityDto> CreateActivityAsync(ActivityDto activityDto)
        {
            var activity = _mapper.Map<Activity>(activityDto);
            await _context.Activities.AddAsync(activity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ActivityDto>(activity);
        }

        public async Task<bool> UpdateActivityAsync(int activityId, ActivityDto activityDto)
        {
            var existingActivity = await _context.Activities.FindAsync(activityId);
            if (existingActivity == null)
                return false;

            _mapper.Map(activityDto, existingActivity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteActivityAsync(int activityId)
        {
            var activity = await _context.Activities.FindAsync(activityId);
            if (activity == null)
                return false;

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
