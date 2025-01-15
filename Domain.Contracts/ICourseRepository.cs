﻿using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICourseRepository : IRepositoryBase<Course>
    {
        Task<IEnumerable<Course>> GetAllWithDetailsAsync();
        Task<Course> GetByIdWithDetailsAsync(int id);
    }
}
