using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using WebApp.Strategy.Entities;
using WebApp.Strategy.Repository.Abstract;

namespace WebApp.Strategy.Repository.Concrete.MongoDb
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly IMongoCollection<TEntity> _collection;
        private readonly IConfiguration configuration;

        public GenericRepository()
        {
            var connectionString = configuration.GetConnectionString("MongoDb");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(configuration.GetValue<string>("MongoDbSettings:DataBaseName"));
            _collection = database.GetCollection<TEntity>(typeof(TEntity).Name.ToLowerInvariant());
        }

        public async Task Delete(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _collection.AsQueryable();
        }

        public async Task<TEntity> GetById(string id)
        {
            return await _collection.Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TEntity> Save(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task Update(TEntity entity)
        {
            await _collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity);
        }
    }
}

