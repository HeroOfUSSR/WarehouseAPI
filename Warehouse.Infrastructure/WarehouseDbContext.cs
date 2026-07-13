using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Models;

namespace Warehouse.Infrastructure
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Storehouse> Warehouses => Set<Storehouse>();
        public DbSet<StockItem> StockItems => Set<StockItem>();
        public DbSet<Movement> Movements => Set<Movement>();
        public DbSet<AppUser> Users => Set<AppUser>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
