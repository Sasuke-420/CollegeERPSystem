using Microsoft.EntityFrameworkCore;

namespace CollegeERPSystem.BusRoute.Domain
{
#nullable disable
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        DbSet<BusRoute.Domain.Models.BusRoute> Routes { get; set; }
    }
}
#nullable enable
