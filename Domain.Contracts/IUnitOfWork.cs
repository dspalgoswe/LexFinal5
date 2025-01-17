namespace Domain.Contracts;

public interface IUnitOfWork
{
    IStudentRepository Student { get; }
    ICourseRepository Course { get; }
    IModuleRepository Module { get; }
    IActivityRepository Activity { get; }
    Task CompleteASync();
    Task<int> SaveChangesAsync();
}