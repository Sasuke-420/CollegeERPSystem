using CollegeERPSystem.Services.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace CollegeERPSystem.Services.Domain.Repositories
{
#nullable disable
    public class MarksRepository
    {
        private readonly IMongoDatabase context;
        private readonly IMongoCollection<Marks> _marks;
        public MarksRepository(MongoDbSettings Context)
        {
            context = Context._db!;
            _marks = context.GetCollection<Marks>(Context.MarksCollection);
        }

        public  Task<IEnumerable<Marks>> GetAllAsync(PaginationModel pagination)
        {
            if (pagination.OrderList == null)
            {
                pagination.OrderList = "Id";
            }
                var constant = Expression.Constant(true);
                var parameter = Expression.Parameter(typeof(Marks), "x");
                var memberExpre = Expression.Property(parameter, pagination.OrderList!);
            

            var express = Expression.Lambda<Func<Marks, string?>>(memberExpre, parameter);

            return  Task.Run(()=> _marks.AsQueryable()
                .Skip((pagination.CurrentPage - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .OrderByDescending(express).AsEnumerable());

        }

        public async Task<Marks> GetByIdAsync(string id)
        {
            return (await _marks.FindAsync(x => x.Id == id))
                                 .FirstOrDefault();
        }

        public async Task<Marks> CreateAsync(Marks marks)
        {
            await _marks.InsertOneAsync(marks);
            return marks;
        }

        public async Task<Marks> UpdateAsync(Marks classModel)
        {
         var result = await _marks.ReplaceOneAsync(x=>x.Id==classModel.Id,classModel);
            return result.IsAcknowledged? classModel: null;
        }
        public async Task<Marks> DeleteAsync(Marks classModel)
        {
          var result =await _marks.DeleteOneAsync(Builders<Marks>.Filter.Eq(x=>x.Id,classModel.Id));

            return result.IsAcknowledged?classModel: null;
        }
    }
}
#nullable enable