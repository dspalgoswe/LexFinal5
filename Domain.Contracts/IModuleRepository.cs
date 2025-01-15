using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IModuleRepository : IRepositoryBase<Module>
    {
        Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId);
        Task<Module> GetModuleByIdWithDetailsAsync(int moduleId);
    }
}
