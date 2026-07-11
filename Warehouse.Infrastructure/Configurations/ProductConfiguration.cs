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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Sku)
                .IsRequired()
                .HasMaxLength(64);

            builder.HasIndex(p => p.Sku)
                .IsUnique();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(p => p.Description)
                .HasMaxLength(2000);

            builder.Property(p => p.Unit)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(p => p.IsActive)
                .IsRequired();
        }
    }
}
