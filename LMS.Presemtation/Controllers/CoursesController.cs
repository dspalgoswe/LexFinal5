using LMS.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Presemtation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly LmsContext _context;

        public CoursesController(LmsContext context)
        {
            _context = context;
        }

        [HttpGet("{courseId}/participants")]
        public async Task<IActionResult> GetCourseParticipants(int courseId)
        {
            //Hämtar deltagare som är kopplade till kursen
            var paticipants = await _context.Users
                .Include(c => c.Courses)
                        //.ThenInclude(m => m.Activities)
                        //.ThenInclude(a => a.ActivityType)
                        //.Include(c => c.Users)
                        //.Include(c => c.Documents)
                        .ToListAsync();


            return Ok(paticipants);
        }

    }
}
