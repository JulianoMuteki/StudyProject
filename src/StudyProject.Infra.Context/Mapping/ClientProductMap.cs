using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyProject.Domain.Entities;

namespace StudyProject.Infra.Context.Mapping
{
    public class ClientProductMap : IEntityTypeConfiguration<ClientProductValue>
    {
        public void Configure(EntityTypeBuilder<ClientProductValue> builder)
        {
            builder.ToTable("ClientsProducts");

            builder.HasKey(t => new { t.ClientID, t.ProductID });

            builder.Property(e => e.Price)
                .IsRequired();

            builder.HasOne(tk => tk.Client)
                .WithMany(t => t.ClientsProductsValues)
                .HasForeignKey(tk => tk.ClientID);

            builder.HasOne(tk => tk.Product)
                .WithMany(k => k.ClientsProductsValues)
                .HasForeignKey(tk => tk.ProductID);
        }
    }
}
