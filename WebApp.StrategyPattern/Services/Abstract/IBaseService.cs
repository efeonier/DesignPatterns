using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.StrategyPattern.Entities;

namespace WebApp.StrategyPattern.Services.Abstract;

public interface IBaseService<TEntity> where TEntity : class, IEntity
{
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> GetByIdAsync(string id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(string id, TEntity entity);
    Task DeleteAsync(string id);
}