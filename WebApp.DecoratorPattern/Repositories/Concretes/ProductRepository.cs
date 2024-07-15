using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.DecoratorPattern.Context;
using WebApp.DecoratorPattern.Entities;
using WebApp.DecoratorPattern.Repositories.Interfaces;

namespace WebApp.DecoratorPattern.Repositories.Concretes;

public class ProductRepository : IProductRepository {
    private readonly AppIdentityDbContext _context;
    public ProductRepository(AppIdentityDbContext context)
    {
        _context = context;
    }
    public async Task<Product> GetById(int id)
    {
        return await _context.Products.FindAsync(id);
    }
    public async Task<List<Product>> GetAll()
    {
        return await _context.Products.ToListAsync();
    }
    public async Task<List<Product>> GetAll(string userId)
    {
        return await _context.Products.Where(w => w.UserId == userId).ToListAsync();
    }
    public async Task<Product> Create(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }
    public async Task Update(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
    public async Task Delete(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
