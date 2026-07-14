using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Models;

namespace Warehouse.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<AppUser?> GetByEmailAsync(string email, CancellationToken ct);
        Task AddAsync(AppUser user, CancellationToken ct);
    }
}
