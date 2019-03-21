using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyProject.Domain.Entities;

namespace StudyProject.Infra.Context.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.ProductID);

            builder.Property(e => e.Name)
          .IsRequired()
          .HasMaxLength(250);

            builder.Property(e => e.Value)
          .IsRequired();

            builder.Property(e => e.Available)
                .IsRequired();

            builder.HasOne(d => d.Client)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clients_Products");
        }
    }
}