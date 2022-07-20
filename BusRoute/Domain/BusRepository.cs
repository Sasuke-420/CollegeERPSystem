using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace CollegeERPSystem.BusRoute.Domain
{
    public class BusRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly DbSet<CollegeERPSystem.BusRoute.Domain.Models.BusRoute> _busRoute;
        public BusRepository(IMapper Mapper,AppDbContext Context)
        {
            mapper = Mapper;
            context = Context;
            _busRoute = context.Set<CollegeERPSystem.BusRoute.Domain.Models.BusRoute>();
        }

        public async Task<IEnumerable<CollegeERPSystem.BusRoute.Domain.Models.BusRoute>> GetAllAsync()
        {
          return  await _busRoute
            .AsNoTracking()
            .ToListAsync();
        }
    }
}
