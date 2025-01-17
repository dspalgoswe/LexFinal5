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
        public async Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId)
        {
            var modules = await _uow.Module.GetModuleByIdAsync(courseId);
            return (IEnumerable<Module>)_mapper.Map<ModuleDto>(modules);
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


        public Task<bool> RemoveUserFromCourseAsync(int courseId, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Course> UpdateCourseAsync(int courseId, CourseDto courseDto)
        {
            var course = await _uow.Course.GetCourseByIdAsync(courseId);
            if (course == null)
            {
                return null;
            }

            // Map updated properties from DTO to the entity
            _mapper.Map(courseDto, course);
            _uow.Course.Update(course);
            await _uow.CompleteASync();

            return course;
        }
    }
}
