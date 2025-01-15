using AutoMapper;
using Domain.Models.Entities;
using LMS.Shared.DTOs;
namespace LMS.Infrastructure.Data;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserForRegistrationDto, ApplicationUser>();
        //CreateMap<ApplicationUser, UserDto>()
        //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
        //    .ForMember(dest => dest.EnrolledCourses, opt => opt.MapFrom(src =>
        //        src.Course?.ToList().Select(c => c.Name) ?? new List<string>()));

        //// Course mappings
        //CreateMap<Course, CourseDto>()
        //    .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
        //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        //    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
        //    .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
        //    .ForMember(dest => dest.Modules, opt => opt.MapFrom(src => src.Modules))
        //    .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.Documents));

        //CreateMap<CreateCourseDto, Course>()
        //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        //    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate));

        //// Module mappings
        //CreateMap<Module, ModuleDto>()
        //    .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.ModuleId))
        //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        //    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
        //    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
        //    .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
        //    .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.Documents));

        //CreateMap<CreateModuleDto, Module>()
        //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        //    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
        //    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));

        //// Activity mappings
        //CreateMap<Activity, ActivityDto>()
        //    .ForMember(dest => dest.ActivityId, opt => opt.MapFrom(src => src.ActivityId))
        //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        //    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
        //    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
        //    .ForMember(dest => dest.ActivityType, opt => opt.MapFrom(src => src.ActivityType))
        //    .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.Documents));

        //CreateMap<CreateActivityDto, Activity>()
        //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        //    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
        //    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));

        //// Document mappings
        //CreateMap<Document, DocumentDto>()
        //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        //    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
        //    .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => src.TimeStamp))
        //    .ForMember(dest => dest.ApplicationUserId, opt => opt.MapFrom(src => src.ApplicationUserId));

        //CreateMap<CreateDocumentDto, Document>()
        //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        //    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
        //    .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}

