using Microsoft.EntityFrameworkCore;
using SettlementApiService.Enitties;

namespace SettlementApiService.Db
{
    public class SettlementDbContext : DbContext
    {
        public SettlementDbContext(DbContextOptions<SettlementDbContext> options) : base(options)
        {
        }

        public DbSet<SettlementBooking> SettlementBookings { get; set; }
    }
}
