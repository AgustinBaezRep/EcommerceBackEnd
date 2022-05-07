﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data.Configurations
{
    public partial class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> entity)
        {
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.IdAdmin).HasColumnName("id_admin");

            entity.Property(e => e.IdClient).HasColumnName("id_client");

            entity.Property(e => e.Issue1).HasColumnName("issue");

            entity.Property(e => e.IssueDate)
                .HasColumnType("datetime")
                .HasColumnName("issue_date");

            entity.Property(e => e.Response).HasColumnName("response");

            entity.Property(e => e.ResponseDate)
                .HasColumnType("datetime")
                .HasColumnName("response_date");

            entity.HasOne(d => d.IdAdminNavigation)
                .WithMany(p => p.IssueIdAdminNavigation)
                .HasForeignKey(d => d.IdAdmin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Issue__id_admin__5812160E");

            entity.HasOne(d => d.IdClientNavigation)
                .WithMany(p => p.IssueIdClientNavigation)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Issue__id_client__571DF1D5");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Issue> entity);
    }
}
