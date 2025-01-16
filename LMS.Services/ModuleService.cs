using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Infrastructure.Data;
using LMS.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ModuleService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<ModuleDto> GetModuleByIdAsync(int moduleId)
        {
            Module? module = await _uow.Module.GetModuleByIdAsync(moduleId);
            return _mapper.Map<ModuleDto>(module);
        }

        public async Task<ModuleDto> CreateModuleAsync(ModuleDto dto)
        {
            Module module = _mapper.Map<Module>(dto);

            _uow.Module.Create(module);

            await _uow.CompleteASync();

            return _mapper.Map<ModuleDto>(module);
        }

        public Task<IEnumerable<ModuleDto>> GetAllModulesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ModuleDto>> GetModulesByCourseIdAsync(int courseId)
        {
            throw new NotImplementedException();
        }
      
        public Task<bool> UpdateModuleAsync(int moduleId, ModuleDto moduleDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteModuleAsync(int moduleId)
        {
            throw new NotImplementedException();
        }
    }
}
