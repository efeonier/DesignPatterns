using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.StrategyPattern.Entities;
using WebApp.StrategyPattern.Repository.Abstract;
using WebApp.StrategyPattern.Services.Abstract;

namespace WebApp.StrategyPattern.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public IQueryable<Product> Get(Expression<Func<Product, bool>> predicate = null)
        {
            return _productRepository.Get(predicate);
        }

        public async Task<Product> GetAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _productRepository.GetAsync(predicate);
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<Product> AddAsync(Product entity)
        {
            return await _productRepository.AddAsync(entity);
        }

        public async Task<Product> UpdateAsync(string id, Product entity)
        {
            return await _productRepository.UpdateAsync(id, entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<List<Product>> GetAllByUserId(string userId)
        {
            return await _productRepository.GetAllByUserId(userId);
        }
    }
}