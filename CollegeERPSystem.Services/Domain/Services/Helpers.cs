using CollegeERPSystem.Services.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeERPSystem.Services.Domain.Services
{
    public class Helpers 
    {
        private readonly AppDbContext context;
        public Helpers()
        {
            context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().EnableSensitiveDataLogging().
                           UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).UseNpgsql(Constants.ConnString).Options);

        }
    /*    public async ValueTask DisposeAsync()
        {
           await context.DisposeAsync();
        }*/

        public async Task<IEnumerable<Programme>> GetProgrammeWithNoStudents()
        {
            return await context.Set<Programme>().Where(x => x.Students!.Count == 0).ToListAsync();
        }
    }
}
