using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyProject.Domain.Entities;

namespace StudyProject.Infra.Context.Mapping
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(e => e.ClientID);

            //builder.Property(e => e.ProductId).HasColumnName("ProductID");

            //builder.Property(e => e.CategoryId).HasColumnName("CategoryID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.IsActive)
                .IsRequired();

            builder.Property(e => e.CreateDate)
                .IsRequired();

            builder.Property(e => e.Email)
                .IsRequired().HasMaxLength(50);

            builder.Property(e => e.LastName)
               .IsRequired()
               .HasMaxLength(250);

            //builder.Property(e => e.QuantityPerUnit).HasMaxLength(20);

            //builder.Property(e => e.ReorderLevel).HasDefaultValueSql("((0))");

            //builder.Property(e => e.SupplierId).HasColumnName("SupplierID");

            //builder.Property(e => e.UnitPrice)
            //    .HasColumnType("money")
            //    .HasDefaultValueSql("((0))");

            //builder.Property(e => e.UnitsInStock).HasDefaultValueSql("((0))");

            //builder.Property(e => e.UnitsOnOrder).HasDefaultValueSql("((0))");

            //builder.HasOne(d => d.Category)
            //    .WithMany(p => p.Products)
            //    .HasForeignKey(d => d.CategoryId)
            //    .HasConstraintName("FK_Products_Categories");

            //builder.HasOne(d => d.Supplier)
            //    .WithMany(p => p.Products)
            //    .HasForeignKey(d => d.SupplierId)
            //    .HasConstraintName("FK_Products_Suppliers");
        }
    }
}