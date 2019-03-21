using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.Infra.Context.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
                builder.ToTable("Products");

                builder.HasKey(e => e.ID).HasName("ProductID");

                builder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                builder.Property(e => e.CreationDate)
                    .IsRequired();

                builder.Property(e => e.DateModified)
                    .IsRequired();

                builder.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                builder.Property(e => e.Weight)
                     .HasColumnType("float")
                    .IsRequired();

            }
        }
}
