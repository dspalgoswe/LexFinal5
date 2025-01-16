using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Infrastructure.Data;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CourseService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
         public async Task<CourseDto> GetCourseByIdAsync(int courseId)
        {
            Course? course = await _uow.Course.GetCourseByIdAsync(courseId);
            if (course == null)
            {
                // Do something
            }
            return _mapper.Map<CourseDto>(course);
        }
        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _uow.Course.GetAllCoursesAsync();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }
        public async Task<CourseDto> CreateCourseAsync(CourseDto courseDto)
        {
            Course course = _mapper.Map<Course>(courseDto);

            _uow.Course.Create(course);

            await _uow.CompleteASync();

            return _mapper.Map<CourseDto>(course);
        }
        public Task<bool> AddUserToCourseAsync(int courseId, string userId)
        {
            throw new NotImplementedException();
        }
             

        public Task<bool> DeleteCourseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Course>> GetCoursesByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserFromCourseAsync(int courseId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Course> UpdateCourseAsync(int id, CourseDto courseDto)
        {
            throw new NotImplementedException();
        }
    }
}
