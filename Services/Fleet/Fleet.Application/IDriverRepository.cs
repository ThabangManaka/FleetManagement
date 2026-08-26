using Fleet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fleet.Application
{
    public interface IDriverRepository
    {
        Task<Driver?> GetByIdAsync(Guid id);

        Task<List<Driver>> GetAllAsync();

        Task AddAsync(Driver driver);

        void Update(Driver driver);

        void Delete(Driver driver);

        Task<bool> ExistsAsync(Guid id);

        Task SaveChangesAsync();
    }
}
