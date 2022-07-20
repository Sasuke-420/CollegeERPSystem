using CollegeERPSystem.Services.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CollegeERPSystem.Services.Domain.Repositories
{
#nullable disable
    public class ProgrammeRepository
    {
        private readonly AppDbContext context;
        private readonly DbSet<Programme> _programme;
        public ProgrammeRepository(AppDbContext Context)
        {
            context = Context;
            _programme = context.Set<Programme>();
        }

        public async Task<IEnumerable<Programme>> GetAllAsync()
        {
            return await _programme.Where(x => x.IsDeleted == false)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<Programme> GetByIdAsync(int id)
        {
            return await _programme.Where(x => x.Id == id && x.IsDeleted == false)
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync();
        }

        public async Task<Programme> CreateAsync(Programme programme)
        {
            EntityEntry<Programme> entity = await _programme.AddAsync(programme).ConfigureAwait(false);
            await context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<Programme> UpdateAsync(Programme programme)
        {
            EntityEntry<Programme> entity = _programme.Update(programme);
            await context.SaveChangesAsync();
            return entity.Entity;
        }
        public async Task<Programme> DeleteAsync(Programme programme)
        {
            programme.IsDeleted = true;
            EntityEntry<Programme> entity = _programme.Update(programme);
            await context.SaveChangesAsync();
            return entity.Entity;
        }
    }
}
#nullable enable