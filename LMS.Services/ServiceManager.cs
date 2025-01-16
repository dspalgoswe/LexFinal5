using AutoMapper;
using Domain.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services;
public class ServiceManager : IServiceManager
{
    // Todo add lazy for all active repos and services

    private readonly Lazy<ICourseService> courseService;
    private readonly Lazy<IActivityService> activityService;

    private readonly Lazy<IModuleService> moduleService;
    private readonly Lazy<IAuthService> authService;

    public IAuthService AuthService => authService.Value;
    public IActivityService ActivityService => activityService.Value;
    public IModuleService ModuleService => moduleService.Value;
    public ICourseService CourseService => courseService.Value;

    public ServiceManager(Lazy<IAuthService> authService, IUnitOfWork uow, IMapper mapper)
    {
        this.authService = authService;
        activityService = new Lazy<IActivityService>(() => new ActivityService(uow, mapper));
        moduleService = new Lazy<IModuleService>(() => new ModuleService(uow, mapper));
        courseService = new Lazy<ICourseService>(() => new CourseService(uow, mapper));
    }
}
