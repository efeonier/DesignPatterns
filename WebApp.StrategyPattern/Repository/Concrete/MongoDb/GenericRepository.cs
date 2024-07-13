using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using WebApp.StrategyPattern.Entities;
using WebApp.StrategyPattern.Repository.Abstract;

namespace WebApp.StrategyPattern.Repository.Concrete.MongoDb
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly IMongoCollection<TEntity> _collection;

        protected GenericRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(configuration.GetValue<string>("MongoDbSettings:DataBaseName"));
            _collection = database.GetCollection<TEntity>(typeof(TEntity).Name.ToLowerInvariant());
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null)
        {
            return MongoQueryableGet(predicate).AsQueryable();
        }

        private IMongoQueryable<TEntity> MongoQueryableGet(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null
                ? _collection.AsQueryable()
                : _collection.AsQueryable().Where(predicate);
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.Find(predicate).FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await _collection.Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };

            await _collection.InsertOneAsync(entity, options);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(string id, TEntity entity)
        {
            return await _collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }
    }
}