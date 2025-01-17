using Domain.Contracts;
using LMS.Infrastructure.Data;

namespace LMS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LmsContext _context;
    private ICourseRepository? _courseRepository;
    private IModuleRepository? _moduleRepository;
    private IActivityRepository? _activityRepository;
    private IStudentRepository? _studentRepository;
      

 

    public UnitOfWork(LmsContext context)
    {
        _context = context;    }

    public ICourseRepository Course => throw new NotImplementedException();

    public IModuleRepository Module => throw new NotImplementedException();

    public IActivityRepository Activity => throw new NotImplementedException();

    public IStudentRepository Student => throw new NotImplementedException();

    public Task CompleteASync()
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
