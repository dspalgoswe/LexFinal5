using Domain.Contracts;
using LMS.Infrastructure.Data;

namespace LMS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LmsContext _context;
    private ICourseRepository _courseRepository;
    private IModuleRepository _moduleRepository;
    private IActivityRepository _activityRepository;

    public UnitOfWork(LmsContext context)
    {
        _context = context;
    }

    public ICourseRepository Course =>
        _courseRepository ??= new CourseRepository(_context);

    public IModuleRepository Module =>
        _moduleRepository ??= new ModuleRepository(_context);

    public IActivityRepository Activity =>
        _activityRepository ??= new ActivityRepository(_context);

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public Task CompleteASync()
    {
        throw new NotImplementedException();
    }
}
