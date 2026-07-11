using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Models;

namespace Warehouse.Infrastructure.Configurations
{
    public class StockItemConfiguration : IEntityTypeConfiguration<StockItem>
    {
        public void Configure(EntityTypeBuilder<StockItem> builder)
        {
            builder.ToTable("stock_items");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Quantity).IsRequired();
            builder.Property(s => s.MinQuantity).IsRequired();
            builder.Property(s => s.UpdatedAt).IsRequired();

            // одна запись на пару товар-склад
            builder.HasIndex(s => new { s.ProductId, s.StorehouseId })
                .IsUnique();

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Storehouse>()
                .WithMany()
                .HasForeignKey(s => s.StorehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
