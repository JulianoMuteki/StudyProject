using Microsoft.EntityFrameworkCore;
using StudyProject.Domain.Entities;
using StudyProject.Infra.Context.Mapping;

namespace StudyProject.Infra.Context
{
    public class StudyProjectContext : DbContext
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
        }
    }
}