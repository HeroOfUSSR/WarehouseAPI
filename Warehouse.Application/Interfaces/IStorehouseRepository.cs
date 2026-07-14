using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Models;

namespace Warehouse.Application.Interfaces
{
    public interface IStorehouseRepository
    {
        Task<Storehouse?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<List<Storehouse>> GetAllAsync(CancellationToken ct);
        Task AddAsync(Storehouse storehouse, CancellationToken ct);
        void Remove(Storehouse storehouse);
    }
}
