using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SportsStoreCore21WebApp.Models.Abstract;
using SportsStoreCore21WebApp.Models.Entities;

namespace SportsStoreCore21WebApp.Models.Concrete
{
  public class EfProductRepository : IProductRepository
  {
    private SportsStoreDbContext _context;
    private readonly ILogger<EfProductRepository> _logger;

    public EfProductRepository(SportsStoreDbContext sportsStoreDbContext, ILogger<EfProductRepository> logger, IConfiguration configuration)
    {
      _context = sportsStoreDbContext;
      _logger = logger;
    }

    public void ClearCache()
    {
      throw new NotImplementedException();
    }

    public Task CreateAsync(Product product)
    {
      throw new NotImplementedException();
    }

    public Task DeleteAsync(int productId)
    {
      throw new NotImplementedException();
    }

    public Task<Product> FindProductByIdAsync(int productId)
    {
      throw new NotImplementedException();
    }

    public Task<List<Product>> FindProductsByCategoryAsync(string category)
    {
      throw new NotImplementedException();
    }

    public Task<List<Product>> GetAllProductsAsync()
    {
      throw new NotImplementedException();
    }

    public Task UpdateAsync(Product product)
    {
      throw new NotImplementedException();
    }
  }
}
