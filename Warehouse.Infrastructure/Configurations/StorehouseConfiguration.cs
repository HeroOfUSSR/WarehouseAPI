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
    public class StorehouseConfiguration : IEntityTypeConfiguration<Storehouse>
    {
        public void Configure(EntityTypeBuilder<Storehouse> builder)
        {
            builder.ToTable("storehouses");

            builder.HasKey(w => w.Id);

            builder.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(w => w.Address)
                .HasMaxLength(512);

            builder.Property(w => w.IsActive)
                .IsRequired();
        }
    }
}
