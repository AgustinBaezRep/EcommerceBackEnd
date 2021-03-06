// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data.Configurations
{
    public partial class UsersConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> entity)
        {
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.DateBirth)
                .HasColumnType("date")
                .HasColumnName("date_birth");

            entity.Property(e => e.Dni).HasColumnName("dni");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");

            entity.Property(e => e.IdHome).HasColumnName("id_home");

            entity.Property(e => e.IdRol).HasColumnName("id_rol");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .HasColumnName("password");

            entity.Property(e => e.Phone).HasColumnName("phone");

            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");

            entity.HasOne(d => d.IdHomeNavigation)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.IdHome)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__id_home__4AB81AF0");

            entity.HasOne(d => d.IdRolNavigation)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__id_rol__4BAC3F29");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Users> entity);
    }
}
