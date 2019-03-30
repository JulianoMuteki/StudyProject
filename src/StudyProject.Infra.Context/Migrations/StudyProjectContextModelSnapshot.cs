﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudyProject.Infra.Context;

namespace StudyProject.Infra.Context.Migrations
{
    [DbContext(typeof(StudyProjectContext))]
    partial class StudyProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StudyProject.Domain.Entities.Client", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("ID")
                        .HasName("ClientID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("StudyProject.Domain.Entities.ClientProductValue", b =>
                {
                    b.Property<Guid>("ClientID");

                    b.Property<Guid>("ProductID");

                    b.Property<float>("Price");

                    b.HasKey("ClientID", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("ClientsProducts");
                });

            modelBuilder.Entity("StudyProject.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("ID")
                        .HasName("ProductID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("StudyProject.Domain.Entities.ClientProductValue", b =>
                {
                    b.HasOne("StudyProject.Domain.Entities.Client", "Client")
                        .WithMany("ClientsProductsValues")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudyProject.Domain.Entities.Product", "Product")
                        .WithMany("ClientsProductsValues")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
