using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.StrategyPattern.Entities;

namespace WebApp.StrategyPattern.Repository.Abstract;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<List<Product>> GetAllByUserId(string userId);
}
