using LMS.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Presemtation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly LmsContext _context;

        public StudentController(LmsContext context)
        {
            _context = context;
        }

        [HttpGet("{studentId}/course")]
        public async Task<IActionResult> GetStudentCourse(string studentId)
        {
            var course = await _context.Courses.Include(c => c.EnrolledUsers)
                        .FirstOrDefaultAsync(c => c.EnrolledUsers.Any(u => u.Id == studentId));

            if (course == null)
            {
                return NotFound("Student is not registered in any of courses");
            }

            return Ok(
                new
                {
                    course.CourseId,
                    course.Name,
                    course.Description,
                    Modules = course.Modules.Select(m => new
                    {
                        m.Name,
                        m.StartDate,
                        m.EndDate
                    })

                });
        }

       

    }
}
