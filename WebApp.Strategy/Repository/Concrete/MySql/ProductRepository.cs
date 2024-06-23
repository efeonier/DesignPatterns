using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Strategy.Context;
using WebApp.Strategy.Entities;
using WebApp.Strategy.Repository.Abstract;

namespace WebApp.Strategy.Repository.Concrete.MySql
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