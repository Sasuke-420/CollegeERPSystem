using CollegeERPSystem.Services.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace CollegeERPSystem.Services.Domain.Repositories
{
#nullable disable
    public class ClassRepository
    {
        private readonly AppDbContext context;
        private readonly DbSet<Class> _org;
        public ClassRepository(AppDbContext Context)
        {
            context = Context;
            _org = context.Set<Class>();
        }

        public async Task<IEnumerable<Class>> GetAllAsync(PaginationModel pagination)
        {

            var constant = Expression.Constant(true);
            var parameter = Expression.Parameter(typeof(Class), "x");
            var memberExpre = Expression.Property(parameter, pagination.OrderList);

            var express = Expression.Lambda<Func<Class, int?>>(memberExpre, parameter);

            return await _org.Where(x => x.IsDeleted == false)
                .Skip((pagination.CurrentPage - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .OrderByDescending(express)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<Class> GetByIdAsync(int id)
        {
            return await _org.Where(x => x.Id == id && x.IsDeleted==false)
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync();
        }

        public async Task<Class> CreateAsync(Class classModel)
        {
            EntityEntry<Class> entity = await _org.AddAsync(classModel).ConfigureAwait(false);
            await context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<Class> UpdateAsync(Class classModel)
        {
            EntityEntry<Class> entity = _org.Update(classModel);
            await context.SaveChangesAsync();
            return entity.Entity;
        }
        public async Task<Class> DeleteAsync(Class classModel)
        {
            classModel.IsDeleted = true;
            EntityEntry<Class> entity = _org.Update(classModel);
            await context.SaveChangesAsync();
            return entity.Entity;
        }
    }
}
#nullable enable