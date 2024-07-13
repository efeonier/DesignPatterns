using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebApp.StrategyPattern.Entities;
using WebApp.StrategyPattern.Repository.Abstract;

namespace WebApp.StrategyPattern.Repository.Concrete.MongoDb
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Task<List<Product>> GetAllByUserId(string userId)
        {
            var query = Get(w => w.UserId == userId);
            var data = query.ToList();
            return Task.FromResult(data);
        }
    }
}