using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        // GET: api/activity/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity(int id)
        {
            var activity = await _activityService.GetActivityAsync(id);
            if (activity == null)
            {
                return NotFound($"Activity with ID {id} not found.");
            }
            return Ok(activity);
        }

        // POST: api/activity
        [HttpPost]
        public async Task<IActionResult> CreateActivity(ActivityDto activityDto)
        {
            if (activityDto == null)
            {
                return BadRequest("Activity data is required.");
            }
            var createdActivity = await _activityService.CreateActivityAsync(activityDto);
            return CreatedAtAction(nameof(GetActivity), new { id = createdActivity.ActivityId }, createdActivity);
        }

        // PUT: api/activity/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(int id, ActivityDto activityDto)
        {
            if (activityDto == null || id != activityDto.ActivityId)
            {
                return BadRequest("Invalid activity data.");
            }

            var isUpdated = await _activityService.UpdateActivityAsync(id, activityDto);
            if (!isUpdated)
            {
                return NotFound($"Activity with ID {id} not found.");
            }

            return NoContent();
        }

        // DELETE: api/activity/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var isDeleted = await _activityService.DeleteActivityAsync(id);
            if (!isDeleted)
            {
                return NotFound($"Activity with ID {id} not found.");
            }

            return NoContent();
        }

        // GET: api/activity/module/{moduleId}
        [HttpGet("module/{moduleId}")]
        public async Task<IActionResult> GetActivitiesByModuleId(int moduleId)
        {
            var activities = await _activityService.GetActivitiesByModuleIdAsync(moduleId);
            if (activities == null)
            {
                return NotFound($"No activities found for module ID {moduleId}.");
            }
            return Ok(activities);
        }
    }
}
