using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.StrategyPattern.Entities;

namespace WebApp.StrategyPattern.Services.Abstract;

public interface IProductService : IBaseService<Product>
{
    Task<List<Product>> GetAllByUserId(string userId);
}
