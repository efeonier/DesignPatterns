using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Strategy.Context;
using WebApp.Strategy.Entities;
using WebApp.Strategy.Repository.Abstract;

namespace WebApp.Strategy.Repository.Concrete.MsSql
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppIdentityDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Product>> GetAllByUserId(string userId)
        {
            return await GetAll().Where(x => x.Id == userId).ToListAsync();
        }
    }
}