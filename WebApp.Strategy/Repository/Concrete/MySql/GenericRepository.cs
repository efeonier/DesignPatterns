using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Strategy.Context;
using WebApp.Strategy.Entities;
using WebApp.Strategy.Repository.Abstract;

namespace WebApp.Strategy.Repository.Concrete.MySql;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
{
    private readonly AppIdentityDbContext _dbContext;

    public GenericRepository(AppIdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>().AsNoTracking();
    }

    public async Task<TEntity> GetById(string id)
    {
        return await _dbContext.Set<TEntity>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == id);
    }


    public async Task<TEntity> Save(TEntity entity)
    {
        entity.Id = Guid.NewGuid().ToString();
        await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;

    }

    public async Task Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(string id)
    {
        var entity = await GetById(id);
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}

