namespace Services.Contracts;
public interface IServiceManager
{
    ICourseService CourseService { get; }
    IActivityService ActivityService { get; }
    IModuleService ModuleService { get; }
    IAuthService AuthService { get; }
}