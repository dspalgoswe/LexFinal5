using System;
using System.Collections.Generic;
using AutoMapper;
using System.Security.Claims;
using Domain.Models.Entities;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Contracts;

namespace LMS.Presemtation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICourseService _courseService;

        public CoursesController(IMapper mapper, ICourseService courseService)
        {
            _mapper = mapper;
            _courseService = courseService;
        }

        // Both teachers and students can view courses
        [HttpGet]
        [Authorize(Roles = "Teacher,Student")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
        }

        // Only teachers can create courses
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            var result = await _courseService.CreateCourseAsync(course);
            return CreatedAtAction(nameof(GetCourses), _mapper.Map<CourseDto>(result));
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDocumentService _documentService;

        public DocumentsController(IMapper mapper, IDocumentService documentService)
        {
            _mapper = mapper;
            _documentService = documentService;
        }

        // Both teachers and students can view documents
        [HttpGet]
        [Authorize(Policy ="StudentDocumentAccess")]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetDocuments()
        {
            var documents = await _documentService.GetAllDocumentsAsync();
            return Ok(_mapper.Map<IEnumerable<DocumentDto>>(documents));
        }

        // Both teachers and students can upload documents
        //[HttpPost]
        //[Authorize(Policy = "StudentDocumentAccess")]
        //public async Task<ActionResult<DocumentDto>> UploadDocument(CreateDocumentDto documentDto)
        //{
        //    var document = _mapper.Map<Document>(documentDto);
        //    document.ApplicationUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    var result = await _documentService.CreateDocumentAsync(documentDto);
        //    return CreatedAtAction(nameof(GetDocuments), _mapper.Map<DocumentDto>(result));
        //}

        //// Activity CRUD
        //[HttpPost("activities")]
        //public async Task<IActionResult> CreateActivity([FromBody] CreateActivityDto activityDto)
        //{
        //    var activity = await TeacherService.CreateActivity(activityDto);
        //    return CreatedAtAction(nameof(GetModuleActivities), new { moduleId = activity.Module.ModuleId }, activity);
        //}

        //[HttpPut("activities/{activityId}")]
        //public async Task<IActionResult> UpdateActivity(int activityId, [FromBody] UpdateActivityDto activityDto)
        //{
        //    var result = await _teacherService.UpdateActivityAsync(activityId, activityDto);
        //    if (!result)
        //        return NotFound();

        //    return Ok();
        //}

        //[HttpDelete("activities/{activityId}")]
        //public async Task<IActionResult> DeleteActivity(int activityId)
        //{
        //    var result = await TeacherService.DeleteActivityAsync(activityId);
        //    if (!result)
        //        return NotFound();

        //    return Ok();
        //}
    }
}


