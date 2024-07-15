using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.DecoratorPattern.Entities;

namespace WebApp.DecoratorPattern.Repositories.Interfaces;

public interface IProductRepository {
    Task<Product> GetById(int id);
    Task<List<Product>> GetAll();
    Task<List<Product>> GetAll(string userId);
    Task<Product> Create(Product product);
    Task Update(Product product);
    Task Delete(Product product);
}
