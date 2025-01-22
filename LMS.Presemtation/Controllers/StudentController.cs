using AutoMapper;
using LMS.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using LMS.Infrastructure.Data;
using LMS.Services;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using LMS.Presemtation.Controllers;
using System.Threading.Channels;

namespace LMS.Presemtation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {

     

// private readonly LmsContext _context;
private readonly IStudentService _studentService;

public StudentController(IStudentService studentService)
{
    //_context = context;
    _studentService = studentService;
}

//GET: api/students
[HttpGet]
public async Task<IActionResult> GetAllStudents()
{
    var students = await _studentService.GetAllStudentsAsync();
    if (students == null || !students.Any())
    {
        return NotFound("No students found.");
    }

    return Ok(students);
}

//GET: api/students/{id}
[HttpGet("{id}")]
public async Task<IActionResult> GetStudentById(int id)
{
    var student = await _studentService.GetStudentByIdAsync(id);
    if (student == null)
    {
        return NotFound($"Student with ID {id} not found.");
    }

    return Ok(student);
}

//GET: api/student/course
[HttpGet("course")]
public async Task<IActionResult> GetStudentCourse(string id)
{
    var course = await _studentService.GetStudentCourseAsync(id);
    if (course == null)
    {
        return NotFound($"No course found for student with ID {id}.");
    }

    return Ok(course);
}

//GET: api/student/Course/participants
[HttpGet("Course/{courseId}/Participants")]
public async Task<IActionResult> GetCourseParticipants(int courseId)
{
    var participants = await _studentService.GetCourseParticipantsAsync(courseId);
    if (participants == null || !participants.Any())
    {
        return NotFound($"No participants found for course with ID {courseId}.");
    }

    return Ok(participants);
}

//GET: api/student/Modules/{courseId}
[HttpGet("Modules/{courseId}")]
public async Task<IActionResult> GetModulesByCourseId(int courseId)
{
    var modules = await _studentService.GetModulesByCourseIdAsync(courseId);
    if (modules == null || !modules.Any())
    {
        return NotFound($"No modules found for course with ID {courseId}.");
    }

    return Ok(modules);
}


//GET: api/student/Activities/{moduleId}
[HttpGet("Activities/{moduleId}")]
public async Task<IActionResult> GetActivitiesByModuleId(int moduleId)
{
    var activities = await _studentService.GetActivitiesByModuleIdAsync(moduleId);
    if (activities == null || !activities.Any())
    {
        return NotFound($"No activities found for module with ID {moduleId}.");
    }

    return Ok(activities);
}


//GET: api/student/Activity/{activityId}
[HttpGet("Activity/{activityId}")]
public async Task<IActionResult> GetActivityById(int activityId)
{
    var activity = await _studentService.GetActivityByIdAsync(activityId);
    if (activity == null)
    {
        return NotFound($"Activity with ID {activityId} not found.");
    }

    return Ok(activity);
}


//PUT: api/student/{id}
[HttpPut("{id}")]
public async Task<IActionResult> UpdateStudentProfile(int id, [FromBody] UpdateUserDto studentDto)
{
    if (studentDto == null)
    {
        return BadRequest("Student data is null.");
    }

    var result = await _studentService.UpdateStudentProfileAsync(id, studentDto);

    if (!result)
    {
        return NotFound($"Student with ID {id} not found or could not be updated.");
    }

    return NoContent();
}


//DELETE: api/student/{id}
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteStudent(int id)
{
    var result = await _studentService.DeleteStudentAsync(id);

    if (!result)
    {
        return NotFound($"Student with ID {id} not found or could not be deleted.");
    }

    return NoContent();
}





    }
}







//[HttpGet("{studentId}/course")]
//public async Task<IActionResult> GetStudentCourse(string studentId)
//{
//    var course = await _context.Courses.Include(c => c.Users)
//                .FirstOrDefaultAsync(c => c.Users.Any(u => u.Id == studentId));

//Get: api / Student
//[HttpGet]
//public async Task<IActionResult> GetStudentCourse(string studentId)
//{
//    var course = await _context.Courses.Include(c => c.Users)
//                            .FirstOrDefaultAsync(c => c.Users.Any(u => u.Id == studentId));


//    if (course == null)
//    {
//        return NotFound("Student is not registered in any of courses");
//    }

//    return Ok(
//        new
//        {
//            course.CourseId,
//            course.Name,
//            course.Description,
//            Modules = course.Modules.Select(m => new
//            {
//                m.Name,
//                m.StartDate,
//                m.EndDate
//            })

//        });

//}


//public async Task<ModuleDto> GetModuleWithActivitiesAsync(int moduleId)
//{
//    var module = await _context.Modules.Include(m => m.Activities).FirstOrDefaultAsync(m => m.ModuleId == moduleId);

//    if(module == null)
//    {
//        throw new Exception($"Module with ID {moduleId} not found");
//    }

//    return _mapper.Map<ModuleDto>(module);

//}



//}

