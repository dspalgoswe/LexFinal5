using Domain.Contracts;
using LMS.Infrastructure.Data;

namespace LMS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LmsContext _context;
    private readonly Lazy<IActivityRepository> _activityRepository;
    private readonly Lazy<IModuleRepository> _moduleRepository;
    private readonly Lazy<ICourseRepository> _courseRepository;
    public UnitOfWork(LmsContext context)
    {
        _context = context;
        _activityRepository = new Lazy<IActivityRepository>(() => new ActivityRepository(_context));
        _moduleRepository = new Lazy<IModuleRepository>(() => new ModuleRepository(_context));
        _courseRepository = new Lazy<ICourseRepository>(() => new CourseRepository(_context));
    } 
    public ICourseRepository Course => _courseRepository.Value;

    public IModuleRepository Module => _moduleRepository.Value;

    public IActivityRepository Activity => _activityRepository.Value;


    public async Task CompleteASync()
    {
        await _context.SaveChangesAsync();
    }
}

