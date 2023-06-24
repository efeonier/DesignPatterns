using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Strategy.Entities;
using WebApp.Strategy.Repository.Abstract;
using WebApp.Strategy.Services.Abstract;

namespace WebApp.Strategy.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Delete(string id)
        {
            await _productRepository.Delete(id);
        }

        public IQueryable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public async Task<List<Product>> GetAllByUserId(string userId)
        {
            return await _productRepository.GetAllByUserId(userId);
        }

        public async Task<Product> GetById(string id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task<Product> Save(Product entity)
        {
            return await _productRepository.Save(entity);
        }

        public async Task Update(Product entity)
        {
            await _productRepository.Update(entity);
        }
    }
}

