using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Presemtation.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper; 

        public CourseController(IServiceManager serviceManager, IMapper mapper)  // Update constructor
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetOneCourse(int id)
        {
            var courseDto = await _serviceManager.CourseService.GetCourseByIdAsync(id);
            return Ok(courseDto);
        }

        [HttpGet("user")]
        public async Task<ActionResult<CourseDto>> GetCourseForCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var courseDto = await _serviceManager.CourseService.GetCoursesByUserIdAsync(userId);
            return Ok(courseDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCourses()
        {
            var courseDtos = await _serviceManager.CourseService.GetAllCoursesAsync();
            return Ok(courseDtos);
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse(CourseDto courseDto)
        {
            if (courseDto == null) return BadRequest();

            var createdCourseDto = await _serviceManager.CourseService.CreateCourseAsync(courseDto);
            return CreatedAtAction(nameof(GetOneCourse), new { id = createdCourseDto.CourseId }, createdCourseDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CourseDto>> UpdateCourse(int id, CourseDto courseDto)
        {
            if (courseDto == null) return BadRequest();

            var updatedCourse = await _serviceManager.CourseService.UpdateCourseAsync(id, courseDto);
            if (updatedCourse == null) return NotFound();

            return Ok(_mapper.Map<CourseDto>(updatedCourse));
        }

        [HttpPatch("{id}")]
        //public async Task<ActionResult> PatchCourse(int id, JsonPatchDocument<UpdateCourseDto> patchDocument)
        //{
        //    if (patchDocument is null) return BadRequest();

        //    var courseToPatch = new UpdateCourseDto(); // Dummy instance for validation
        //    patchDocument.ApplyTo(courseToPatch, ModelState);

        //    if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

        //    var updatedCourse = await _serviceManager.CourseService.UpdateCourseAsync(id, patchDocument);

        //    return Ok(updatedCourse);
        //}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            await _serviceManager.CourseService.DeleteCourseAsync(id);
            return NoContent();
        }
    }
}

