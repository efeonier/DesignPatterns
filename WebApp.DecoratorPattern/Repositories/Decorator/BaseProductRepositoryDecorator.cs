using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.DecoratorPattern.Entities;
using WebApp.DecoratorPattern.Repositories.Interfaces;

namespace WebApp.DecoratorPattern.Repositories.Decorator;

public class BaseProductRepositoryDecorator : IProductRepository {
    private readonly IProductRepository _productRepository;
    protected BaseProductRepositoryDecorator(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async virtual Task<Product> GetById(int id)
    {
        return await _productRepository.GetById(id);
    }
    public async virtual Task<List<Product>> GetAll()
    {
        return await _productRepository.GetAll();
    }
    public async virtual Task<List<Product>> GetAll(string userId)
    {
        return await _productRepository.GetAll(userId);
    }
    public async virtual Task<Product> Create(Product product)
    {
        return await _productRepository.Create(product);
    }
    public async virtual Task Update(Product product)
    {
        await _productRepository.Update(product);
    }
    public async virtual Task Delete(Product product)
    {
        await _productRepository.Delete(product);
    }
}
