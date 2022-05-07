﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data.Configurations
{
    public partial class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> entity)
        {
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");

            entity.Property(e => e.IdAction).HasColumnName("id_action");

            entity.HasOne(d => d.IdActionNavigation)
                .WithMany(p => p.Rol)
                .HasForeignKey(d => d.IdAction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rol__id_action__286302EC");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Rol> entity);
    }
}