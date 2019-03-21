using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyProject.Domain.Entities;

namespace StudyProject.Infra.Context.Mapping
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(e => e.ID).HasName("ClientID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.CreationDate)
                .IsRequired();

            builder.Property(e => e.DateModified)
                .IsRequired();

        builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Email)
                .IsRequired();
        builder.Property(e => e.IsActive)
                .IsRequired();

        }
    }
}