using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;

namespace Persistence.EntitiesConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired(true);

            builder.HasData(new Role("ADMIN"), new Role("USER"));
        }
    }
}

