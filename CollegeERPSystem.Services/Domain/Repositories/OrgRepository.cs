using CollegeERPSystem.Services.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace CollegeERPSystem.Services.Domain.Repositories
{
#nullable disable
    public class OrgRepository
    {
        private readonly AppDbContext context;
        private readonly DbSet<Org> _org;
        public OrgRepository(AppDbContext Context)
        {
            context = Context;
            _org = context.Set<Org>();
        }

        public async Task<IEnumerable<Org>> GetAllAsync(PaginationModel pagination)
        {

            var constant = Expression.Constant(true);
            var parameter = Expression.Parameter(typeof(Org), "x");
            var memberExpre = Expression.Property(parameter, pagination.OrderList);

            var express = Expression.Lambda<Func<Org, int?>>(memberExpre, parameter);

            return await _org.Where(x => x.IsDeleted == false)
                .Skip((pagination.CurrentPage - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .OrderByDescending(express)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<Org> GetByIdAsync(int id)
        {
            return await _org.Where(x => x.Id == id && x.IsDeleted == false)
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync();
        }

        public async Task<Org> CreateAsync(Org org)
        {
            EntityEntry<Org> entity = await _org.AddAsync(org).ConfigureAwait(false);
            await context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<Org> UpdateAsync(Org org)
        {
            EntityEntry<Org> entity = _org.Update(org);
            await context.SaveChangesAsync();
            return entity.Entity;
        }
        public async Task<Org> DeleteAsync(Org org)
        {
            org.IsDeleted = true;
            EntityEntry<Org> entity = _org.Update(org);
            await context.SaveChangesAsync();
            return entity.Entity;
        }
    }
}
#nullable enable