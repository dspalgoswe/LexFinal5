//using System;
//using System.Collections.Generic;
//using AutoMapper;
//using System.Security.Claims;
//using Domain.Models.Entities;
//using LMS.Shared.DTOs;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Cors.Infrastructure;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Services.Contracts;

//namespace LMS.Presemtation.Controllers
//{
//    [ApiController]
//    [Route("api/teacher")]
//    [Authorize(Policy = "TeacherPolicy")]
//    public class TeacherController : ControllerBase
//    {
//        private readonly IMapper _mapper;
//        private readonly ICourseService _courseService;
//        private readonly IModuleService _moduleService;
//        private readonly IActivityService _activityService;

//        public TeacherController(
//            IMapper mapper,
//            ICourseService courseService,
//            IModuleService moduleService,
//            IActivityService activityService)
//        {
//            _mapper = mapper;
//            _courseService = courseService;
//            _moduleService = moduleService;
//            _activityService = activityService;
//        }

//        // GET: api/teacher/courses
//        // Both teachers and students can view courses
//        [HttpGet("courses")]
//        [Authorize(Roles = "Teacher,Student")]
//        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
//        {
//            var courses = await _courseService.GetAllCoursesAsync();
//            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
//        }

//        // GET: api/teacher/courses/{courseId}/modules
//        [HttpGet("courses/{courseId}/modules")]
//        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModulesByCourse(int courseId)
//        {
//            var modules = await _moduleService.GetModulesByCourseIdAsync(courseId);
//            if (modules == null)
//            {
//                return NotFound();
//            }
//            return Ok(_mapper.Map<IEnumerable<ModuleDto>>(modules));
//        }

//        // GET: api/teacher/modules/{moduleId}/activities
//        [HttpGet("modules/{moduleId}/activities")]
//        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetActivitiesByModule(int moduleId)
//        {
//            var activities = await _activityService.GetActivitiesByModuleIdAsync(moduleId);
//            if (activities == null)
//            {
//                return NotFound();
//            }
//            return Ok(_mapper.Map<IEnumerable<ActivityDto>>(activities));
//        }

//        // POST: api/teacher/courses
//        [HttpPost("courses")]
//        //public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseDto courseDto)
//        //{
//        //    var course = _mapper.Map<Course>(courseDto);
//        //    var result = await _courseService.CreateCourseAsync(Course);
//        //    return CreatedAtAction(nameof(GetCourses), _mapper.Map<CourseDto>(result));
//        //}

//        // POST: api/teacher/modules
//        [HttpPost("modules")]
//        public async Task<ActionResult<ModuleDto>> CreateModule(CreateModuleDto moduleDto)
//        {
//            var module = _mapper.Map<Module>(moduleDto);
//            var result = await _moduleService.CreateModuleAsync(_mapper.Map<ModuleDto>(module)); // Change here
//            return CreatedAtAction(nameof(GetModulesByCourse),
//                new { courseId = moduleDto.CourseId },
//                _mapper.Map<ModuleDto>(result));
//        }

//        // POST: api/teacher/activities
//        [HttpPost("activities")]
//        public async Task<ActionResult<ActivityDto>> CreateActivity(CreateActivityDto activityDto)
//        {
//            var activityDtoMapped = _mapper.Map<ActivityDto>(activityDto); // Change here
//            var result = await _activityService.CreateActivityAsync(activityDtoMapped); // Use the DTO
//            return CreatedAtAction(nameof(GetActivitiesByModule),
//                new { moduleId = activityDto.ModuleId },
//                result); // Already in DTO format
//        }
//    }
//}

