using AutoMapper;
using Domain.Models.Entities;
using LMS.Shared.DTOs;
namespace LMS.Infrastructure.Data;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User mappings
        CreateMap<UserForRegistrationDto, ApplicationUser>();
        CreateMap<ApplicationUser, UserDto>().ReverseMap();

        CreateMap<Course, CourseDto>();
        CreateMap<CourseDto, Course>()
            .ForMember(dest => dest.CourseId, opt => opt.Ignore())
            .ForMember(dest => dest.Modules, opt => opt.MapFrom(src => src.Modules));
        CreateMap<CreateCourseDto, CourseDto>().ReverseMap();

        // Module mappings
        CreateMap<Module, ModuleDto>().ReverseMap();
        CreateMap<ModuleDto, Module>().ReverseMap();


        // Activity mappings
        CreateMap<Activity, ActivityDto>().ReverseMap();
        CreateMap<ActivityDto, Activity>().ReverseMap();


        // ActivityType mappings
        CreateMap<ActivityType, ActivityTypeDto>();
        CreateMap<ActivityTypeDto, ActivityType>();
          

    }
}

