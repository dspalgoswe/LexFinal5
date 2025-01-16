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

        // Module mappings
        CreateMap<Module, ModuleDto>();
        CreateMap<ModuleDto, Module>()
            .ForMember(dest => dest.ModuleId, opt => opt.Ignore())
            .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities));

        // Activity mappings
        CreateMap<Activity, ActivityDto>();
        CreateMap<ActivityDto, Activity>()
            .ForMember(dest => dest.ActivityId, opt => opt.Ignore())
            .ForMember(dest => dest.ActivityType, opt => opt.MapFrom(src => src.ActivityType));

        // ActivityType mappings
        CreateMap<ActivityType, ActivityTypeDto>();
        CreateMap<ActivityTypeDto, ActivityType>()
            .ForMember(dest => dest.ActivityTypeId, opt => opt.Ignore());

    }
}

