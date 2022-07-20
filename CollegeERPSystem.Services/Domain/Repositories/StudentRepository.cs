using CollegeERPSystem.Services.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using System.Reflection;

namespace CollegeERPSystem.Services.Domain.Repositories
{
#nullable disable
    public class StudentRepository
    {
        private readonly AppDbContext context;
        private readonly DbSet<Student> _Student;
        public StudentRepository(AppDbContext Context)
        {
            context = Context;
            _Student = context.Set<Student>();
        }

        public async Task<IEnumerable<Student>> GetAllAsync(PaginationModel pagination)
        {
           
            var constant = Expression.Constant(true);
            var parameter = Expression.Parameter(typeof(Student),"x");
            var memberExpre = Expression.Property(parameter, pagination.OrderList);
            
            var express = Expression.Lambda<Func<Student,int?>>(memberExpre,parameter);

            return await _Student.Where(x => x.IsDeleted == false)
                .Include(x => x.Grade)
                .Include(x => x.Program)
                .Skip((pagination.CurrentPage-1)*pagination.PageSize)
                .Take(pagination.PageSize)
                .OrderByDescending(express)
            .AsNoTracking()
            .ToListAsync();
        }

        public virtual async Task<Student> GetByIdAsync(int id)
        {
            return await _Student.Where(x => x.Id == id && x.IsDeleted == false)
                                 .Include(x => x.Program)
                                 .Include(x => x.Grade)
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync();
        }

        public virtual async Task<Student> CreateAsync(Student student)
        {
            EntityEntry<Student> entity = await _Student.AddAsync(student).ConfigureAwait(false);
            await context.SaveChangesAsync();
            return entity.Entity;
        }

        public virtual async Task<Student> UpdateAsync(Student student)
        {
            EntityEntry<Student> entity = _Student.Update(student);
            await context.SaveChangesAsync();
            return entity.Entity;
        }
        public virtual async Task<Student> DeleteAsync(Student student)
        {
            student.IsDeleted = true;
            EntityEntry<Student> entity = _Student.Update(student);
            await context.SaveChangesAsync();
            return entity.Entity;
        }
    }
}
#nullable enable
