using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Strategy.Entities;

namespace WebApp.Strategy.Services.Abstract
{
    public interface IProductService : IBaseService<Product>
    {
        Task<List<Product>> GetAllByUserId(string userId);

    }
}

