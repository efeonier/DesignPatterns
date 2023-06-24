using System;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Strategy.Entities;

namespace WebApp.Strategy.Repository.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(string id);

        Task<TEntity> Save(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(string id);
    }
}

