using CollegeERPSystem.Services.Domain.Models;
using CollegeERPSystem.Services.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CollegeERPSystem.Services.Domain.Repositories
{
#nullable disable
    public class SchoolRepository
    {
        private readonly AppDbContext context;
        private readonly DbSet<School> _school;
        public SchoolRepository(AppDbContext Context)
        {
            context = Context;
            _school = context.Set<School>();
        }

        public async Task<IEnumerable<School>> GetAllAsync()
        {
            return await _school.Where(x => x.IsDeleted == false)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<School> GetByIdAsync(int id)
        {
            return await _school.Where(x => x.Id == id && x.IsDeleted==false)
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
        }

        public async Task<School> CreateAsync(School school)
        {
            EntityEntry<School> entity = await _school.AddAsync(school).ConfigureAwait(false);
            await context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<School> UpdateAsync(School school)
        {
            EntityEntry<School> entity = _school.Update(school);
            await context.SaveChangesAsync();
            return entity.Entity;
        }
        public async Task<School> DeleteAsync(School school)
        {
            school.IsDeleted = true;
            EntityEntry<School> entity = _school.Update(school);
            await context.SaveChangesAsync();
            return entity.Entity;
        }
    }
}
#nullable enable
