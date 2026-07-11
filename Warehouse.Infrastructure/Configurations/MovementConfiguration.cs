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
    public class MovementConfiguration : IEntityTypeConfiguration<Movement>
    {
        public void Configure(EntityTypeBuilder<Movement> builder)
        {
            builder.ToTable("movements");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Quantity).IsRequired();

            builder.Property(m => m.Type)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(m => m.Comment)
                .HasMaxLength(1000);

            builder.Property(m => m.CreatedAt).IsRequired();

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(m => m.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(m => m.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // две отдельные связи с Warehouse — Source и Destination
            builder.HasOne<Storehouse>()
                .WithMany()
                .HasForeignKey(m => m.SourceStorehouseId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne<Storehouse>()
                .WithMany()
                .HasForeignKey(m => m.DestinationStorehouseId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}
