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

        // Course mappings
        CreateMap<CreateCourseDto, Course>().ReverseMap();
        CreateMap<Course, CourseDto>();


        // Module mappings
        CreateMap<Module, ModuleDto>().ReverseMap();
        CreateMap<CreateModuleDto, Module>();


        // Activity mappings
        CreateMap<Activity, ActivityDto>().ReverseMap();
        CreateMap<CreateActivityDto, Activity>();


        // Document mappings
        CreateMap<Document, DocumentDto>().ReverseMap();
        CreateMap<CreateDocumentDto, Document>();

    }
}

