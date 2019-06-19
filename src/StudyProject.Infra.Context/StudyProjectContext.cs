using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyProject.Domain.Entities;
using StudyProject.Infra.Context.Identity;
using StudyProject.Infra.Context.Mapping;
using System;

namespace StudyProject.Infra.Context
{
    public class StudyProjectContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim,
                                                        ApplicationUserRole, IdentityUserLogin<Guid>,
                                                        ApplicationRoleClaim, IdentityUserToken<Guid>>
    {
        //public DbSet<Blog> Blogs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ClientProductValue> ClientsProducts { get; set; }

        public StudyProjectContext()
        {
        }

        public StudyProjectContext(DbContextOptions<StudyProjectContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Blogging;Trusted_Connection=True;");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ClientProductMap());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });


            modelBuilder.Entity<ApplicationUserClaim>(userClaim =>
            {
                userClaim.HasKey(ur => ur.Id);

                userClaim.HasOne(ur => ur.User)
                    .WithMany(r => r.UserClaims)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });


            modelBuilder.Entity<ApplicationRoleClaim>(roleClaim =>
            {
                roleClaim.HasKey(ur => ur.Id);

                roleClaim.HasOne(ur => ur.Role)
                    .WithMany(r => r.RoleClaims)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });
        }
    }
}