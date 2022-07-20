using CollegeERPSystem.Services.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeERPSystem.Services.Domain
{
#nullable disable
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Class> Classes {get; set;}
        public DbSet<Programme> Programs {get; set;}
        public DbSet<Student> Students {get; set;}
        public DbSet<School> Schools {get; set;}
        public DbSet<Org> Org {get; set;}
    }
}
#nullable enable