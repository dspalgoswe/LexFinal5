using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using LMS.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;

namespace LMS.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public StudentService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllStudentsAsync()
        {
            //var students = await _unitOfWork.Course.GetAllWithDetailsAsync();
            var students = await _uow.Student.GetAllStudentsAsync();
            return _mapper.Map<IEnumerable<UserDto>>(students);
        }


        public async Task<UserDto> GetStudentByIdAsync(int studentId)
        {
            var student = await _uow.Student.GetStudentByIdAsync(studentId);
            return _mapper.Map<UserDto>(student);
        }


        public async Task<CourseDto> GetStudentCourseAsync(string studentId)
        {
           var course = await _uow.Student.GetStudentCourseAsync(studentId);
            return _mapper.Map<CourseDto>(course);
        }

        

        public async Task<List<ActivityDto>> GetActivitiesByModuleIdAsync (int moduleId)
        {
           var activities = await _uow.Activity.GetActivitiesByModuleIdAsync (moduleId);
            return _mapper.Map<List<ActivityDto>>(activities);
        }

        public async Task<ActivityDto> GetActivityByIdAsync(int activityId)
        {
            var activity = await _uow.Activity.GetActivityByIdAsync(activityId);
            return _mapper.Map<ActivityDto>(activity);
        }

        public async Task<List<UserDto>> GetCourseParticipantsAsync(int courseId)
        {
            var courseParticipants = await _uow.Student.GetCourseParticipantsAsync(courseId);
            return _mapper.Map<List<UserDto>>(courseParticipants);
        }

        public async Task<List<ModuleDto>> GetModulesByCourseIdAsync(int courseId)
        {
            var modulesByCourses = await _uow.Module.GetModulesByCourseIdAsync(courseId);
            return _mapper.Map<List<ModuleDto>>(modulesByCourses);
        }



        public async Task<bool> UpdateStudentProfileAsync(int studentId, UpdateUserDto studentDto)
        {
            //Hämtar studenten från databasen via UnitOfWork
            var currentStudent = await _uow.Student.GetStudentByIdAsync(studentId);

            if(currentStudent == null)
            {
                return false; //Studenten finns inte
            }

            //Uppdaterar studentens data med hjälp av AutoMapper
            _mapper.Map(studentDto, currentStudent);

            //Skickar tillbaka ändringarna till databasen
            _uow.Student.Update(currentStudent);

            var result = await _uow.SaveChangesAsync();
            return result > 0;

        }
    }
}
