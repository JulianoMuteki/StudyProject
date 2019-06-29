using CtrlBox.Infra.Context.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyProject.Domain.Entities;

namespace StudyProject.Infra.Context.Mapping
{
    public class ProductMap : EntityConfiguration<Product>
    {
        protected override void Initialize(EntityTypeBuilder<Product> builder)
        {
            base.Initialize(builder);

            builder.ToTable("Products");
            builder.Property(x => x.ID).HasColumnName("ProductID");
            builder.HasKey(b => b.ID).HasName("ProductID");

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
