using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.StrategyPattern.Context;
using WebApp.StrategyPattern.Entities;
using WebApp.StrategyPattern.Repository.Abstract;

namespace WebApp.StrategyPattern.Repository.Concrete.MySql
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppIdentityDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Product>> GetAllByUserId(string userId)
        {
            return await Get(x => x.UserId == userId).ToListAsync();
        }
    }
}