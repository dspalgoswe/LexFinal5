using AutoMapper;
using LMS.Infrastructure.Data;
using LMS.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Contracts;
using Domain.Contracts;

namespace LMS.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public ActivityService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ActivityDto> GetActivityAsync(int activityId)
        {
            Activity? activity = await _uow.Activity.GetActivityByIdAsync(activityId);
            return _mapper.Map<ActivityDto>(activity);
        }
        public Task<ActivityDto> CreateActivityAsync(ActivityDto activityDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteActivityAsync(int activityId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ActivityDto>> GetActivitiesByModuleIdAsync(int moduleId)
        {
            throw new NotImplementedException();
        }


        public Task<bool> UpdateActivityAsync(int activityId, ActivityDto activityDto)
        {
            throw new NotImplementedException();
        }
    }
}
