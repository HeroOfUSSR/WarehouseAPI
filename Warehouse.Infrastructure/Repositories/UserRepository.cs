using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Models;

namespace Warehouse.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WarehouseDbContext _context;

        public UserRepository(WarehouseDbContext context) => _context = context;

        public Task<AppUser?> GetByIdAsync(Guid id, CancellationToken ct)
            => _context.Users.FirstOrDefaultAsync(u => u.Id == id, ct);

        public Task<AppUser?> GetByEmailAsync(string email, CancellationToken ct)
            => _context.Users.FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant(), ct);

        public async Task AddAsync(AppUser user, CancellationToken ct)
            => await _context.Users.AddAsync(user, ct);
    }
}
