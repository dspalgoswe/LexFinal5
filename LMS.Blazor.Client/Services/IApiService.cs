using LMS.Blazor.Client.Models;
using LMS.Shared.DTOs;

namespace LMS.Blazor.Client.Services;

public interface IApiService
{
    Task<IEnumerable<DemoDto>> CallApiAsync();
}

public class MockApiService : IApiService
{
    public Task<IEnumerable<DemoDto>> CallApiAsync()
    {
        throw new NotImplementedException();
    }
}
