﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data.Configurations
{
    public partial class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> entity)
        {
            entity.ToTable("Sub_Category");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");

            entity.Property(e => e.Show).HasColumnName("show");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<SubCategory> entity);
    }
}