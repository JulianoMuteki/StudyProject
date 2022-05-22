using FluentValidation.Results;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyProject.Domain.Entities;
using StudyProject.Domain.Identity;
using StudyProject.Infra.Context.Mapping;
using System;

namespace StudyProject.Infra.Context
{
    public class StudyProjectContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim,
                                                        ApplicationUserRole, ApplicationUserLogin,
                                                        ApplicationRoleClaim, ApplicationUserToken>
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
          // this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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
            modelBuilder.Ignore<ValidationFailure>();
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ClientProductMap());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.UserClaims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.UserLogins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.UserTokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });
        }
    }
}