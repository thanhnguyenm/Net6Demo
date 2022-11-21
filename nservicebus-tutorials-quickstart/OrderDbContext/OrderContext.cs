using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace OrderDbContext
{
    public class OrderContext : DbContext
    {
        private readonly DbContextOptions options;

        public DbSet<Order> Orders { get; set; }

        public OrderContext()
        {

        }

        public OrderContext(DbContextOptions options) : base(options)
        {
            this.options = options;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .ToTable("Order")
                .HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=DESKTOP-8IUGMH6;Initial Catalog=RetailDB;Persist Security Info=False;User Id=sa;Password=123456789x@X;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
