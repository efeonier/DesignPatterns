using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using WebApp.DecoratorPattern.Entities;
using WebApp.DecoratorPattern.Repositories.Interfaces;

namespace WebApp.DecoratorPattern.Repositories.Decorator;

public class ProductRepositoryCacheDecorator : BaseProductRepositoryDecorator {
    private readonly IMemoryCache _memoryCache;
    private const string ProductsCacheName = "products";
    public ProductRepositoryCacheDecorator(IProductRepository productRepository, IMemoryCache memoryCache) : base(productRepository)
    {
        _memoryCache = memoryCache;
    }
    public override async Task<List<Product>> GetAll()
    {
        if (_memoryCache.TryGetValue(ProductsCacheName, out List<Product> cacheProduct)){
            return cacheProduct;
        }
        await UpdateCache();
        return _memoryCache.Get<List<Product>>(ProductsCacheName);
    }
    public override async Task<List<Product>> GetAll(string userId)
    {
        var products = await GetAll();
        return products.Where(w => w.UserId == userId).ToList();
    }
    public override async Task<Product> Create(Product product)
    {
        await base.Create(product);
        await UpdateCache();
        return product;
    }
    public override async Task Update(Product product)
    {
        await base.Update(product);
        await UpdateCache();
    }
    public override async Task Delete(Product product)
    {
        await base.Delete(product);
        await UpdateCache();
    }
    private async Task UpdateCache()
    {
        _memoryCache.Set(ProductsCacheName, await base.GetAll());
    }
}
