using Domain.Models.Entities;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: api/course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        // GET: api/course/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound($"Course with ID {id} not found.");
            }
            return Ok(course);
        }

        // POST: api/course
        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse(CourseDto courseDto)
        {
            if (courseDto == null)
            {
                return BadRequest("Course data is required.");
            }
            var createdCourse = await _courseService.CreateCourseAsync(courseDto);
            return CreatedAtAction(nameof(GetCourseById), new { id = createdCourse.CourseId }, createdCourse);
        }

        // PUT: api/course/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<CourseDto>> UpdateCourse(int id, CourseDto courseDto)
        {
            var updatedCourse = await _courseService.UpdateCourseAsync(id, courseDto);
            if (updatedCourse == null)
            {
                return NotFound($"Course with ID {id} not found.");
            }
            return Ok(updatedCourse);
        }

        // DELETE: api/course/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _courseService.DeleteCourseAsync(id);
            if (!result)
            {
                return NotFound($"Course with ID {id} not found.");
            }
            return NoContent();
        }

        // POST: api/course/{courseId}/users/{userId}
        [HttpPost("{courseId}/users/{userId}")]
        public async Task<IActionResult> AddUserToCourse(int courseId, string userId)
        {
            var result = await _courseService.AddUserToCourseAsync(courseId, userId);
            if (!result)
            {
                return NotFound($"Course or User not found with provided IDs.");
            }
            return Ok();
        }

        // DELETE: api/course/{courseId}/users/{userId}
        [HttpDelete("{courseId}/users/{userId}")]
        public async Task<IActionResult> RemoveUserFromCourse(int courseId, string userId)
        {
            var result = await _courseService.RemoveUserFromCourseAsync(courseId, userId);
            if (!result)
            {
                return NotFound($"Course or User not found with provided IDs.");
            }
            return NoContent();
        }

        // GET: api/course/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByUserId(string userId)
        {
            var courses = await _courseService.GetCoursesByUserIdAsync(userId);
            return Ok(courses);
        }

        // GET: api/course/{courseId}/modules
        [HttpGet("{courseId}/modules")]
        public async Task<ActionResult<IEnumerable<Module>>> GetModulesByCourseId(int courseId)
        {
            var modules = await _courseService.GetModulesByCourseIdAsync(courseId);
            return Ok(modules);
        }
    }
}
