namespace Domain.Contracts;

public interface IUnitOfWork
{
    ICourseRepository Course { get; }
    IModuleRepository Module { get; }
    IActivityRepository Activity { get; }
    Task CompleteASync();
}