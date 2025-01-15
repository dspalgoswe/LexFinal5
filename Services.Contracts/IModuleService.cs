using Domain.Models.Entities;
using LMS.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IModuleService
    {
        Task<IEnumerable<ModuleDto>> GetAllModulesAsync();
        Task<IEnumerable<ModuleDto>> GetModulesByCourseIdAsync(int courseId);
        Task<ModuleDto> GetModuleByIdAsync(int moduleId);
        Task<ModuleDto> CreateModuleAsync(ModuleDto moduleDto);
        Task<bool> UpdateModuleAsync(int moduleId, ModuleDto moduleDto);
        Task<bool> DeleteModuleAsync(int moduleId);
    }
}
