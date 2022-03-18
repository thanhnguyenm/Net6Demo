using Microsoft.EntityFrameworkCore;

namespace UploadLargeFile.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server=localhost;Database=Demo;User ID=sa;Password=123456789x@X;TrustServerCertificate=True");

            base.OnConfiguring(optionsBuilder); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Instrument> Instruments { get; set; }
    }
}
