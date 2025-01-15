using AutoMapper;
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
        private readonly LmsContext _context;
        private readonly IMapper _mapper;

        public ModuleService(LmsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ModuleDto>> GetAllModulesAsync()
        {
            var modules = await _context.Modules
                .Include(m => m.Activities)
                .Include(m => m.Documents)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ModuleDto>>(modules);
        }

        public async Task<IEnumerable<ModuleDto>> GetModulesByCourseIdAsync(int courseId)
        {
            var modules = await _context.Modules
                .Include(m => m.Activities)
                .Include(m => m.Documents)
                .Where(m => m.CourseId == courseId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ModuleDto>>(modules);
        }

        public async Task<ModuleDto> GetModuleByIdAsync(int moduleId)
        {
            var module = await _context.Modules
                .Include(m => m.Activities)
                .Include(m => m.Documents)
                .FirstOrDefaultAsync(m => m.ModuleId == moduleId);
            return _mapper.Map<ModuleDto>(module);
        }

        public async Task<ModuleDto> CreateModuleAsync(ModuleDto moduleDto)
        {
            var module = _mapper.Map<Module>(moduleDto);
            await _context.Modules.AddAsync(module);
            await _context.SaveChangesAsync();
            return _mapper.Map<ModuleDto>(module);
        }

        public async Task<bool> UpdateModuleAsync(int moduleId, ModuleDto moduleDto)
        {
            var existingModule = await _context.Modules
                .Include(m => m.Activities)
                .Include(m => m.Documents)
                .FirstOrDefaultAsync(m => m.ModuleId == moduleId);

            if (existingModule == null)
                return false;

            _mapper.Map(moduleDto, existingModule);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteModuleAsync(int moduleId)
        {
            var module = await _context.Modules.FirstOrDefaultAsync(m => m.ModuleId == moduleId);
            if (module == null)
                return false;

            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
