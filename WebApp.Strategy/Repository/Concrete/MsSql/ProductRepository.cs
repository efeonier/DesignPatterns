using System.Collections.Generic;
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
            var data = await Get(w => w.UserId == userId).ToListAsync();
            return data;
        }
    }
}