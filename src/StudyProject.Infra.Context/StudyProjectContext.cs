using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.Infra.Context
{
    public class StudyProjectContext : DbContext
    {
        //public DbSet<Blog> Blogs { get; set; }
        //public DbSet<Post> Posts { get; set; }

        public StudyProjectContext()
        {

        }

        public StudyProjectContext(DbContextOptions<StudyProjectContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Blogging;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //https://stackoverflow.com/questions/37493095/entity-framework-core-rc2-table-name-pluralization
            modelBuilder.RemovePluralizingTableNameConvention();

            //https://medium.com/agilix/entity-framework-core-enums-ee0f8f4063f2
            //https://medium.com/@kamgrzybek/domain-model-encapsulation-and-pi-with-entity-framework-2-2-f4a52e892ff5
            //http://www.kamilgrzybek.com/design/domain-model-encapsulation-and-pi-with-entity-framework-2-2/
            //modelBuilder.Entity<Blog>(entity =>
            //{
            //    entity.Property(e => e.Url).IsRequired();
            //});

            //modelBuilder.Entity<Post>(entity =>
            //{
            //    entity.HasOne(d => d.Blog)
            //        .WithMany(p => p.Post)
            //        .HasForeignKey(d => d.BlogId);
            //});
        }
    }
}
