using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SportsStoreCore21WebApp.Models.Abstract;
using SportsStoreCore21WebApp.Models.Entities;

namespace SportsStoreCore21WebApp.Models.Concrete
{
  public class EfProductRepository : IProductRepository, IDisposable
  {
    private SportsStoreDbContext _context;
    private readonly ILogger<EfProductRepository> _logger;
    private readonly IConfiguration _configuration;

    public EfProductRepository(SportsStoreDbContext sportsStoreDbContext, ILogger<EfProductRepository> logger, IConfiguration configuration)
    {
      _context = sportsStoreDbContext;
      _logger = logger;
      _configuration = configuration;
    }

    #region IProductRepository Members
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

    public async Task<List<Product>> GetAllProductsAsync()
    {
      try
      {
        var productsList = await _context.Products.ToListAsync();
        _logger.LogInformation($"MN ---- ")
      }
      catch (Exception ex)
      {

        throw;
      }
    }

    public Task UpdateAsync(Product product)
    {
      throw new NotImplementedException();
    }


    #endregion

    #region IDispose Member
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    } 
    #endregion

    private void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (_context != null)
        {
          _context.Dispose();
          _context = null;
        }
      }
    }

    private void LogInfo(string message)
    {
      _logger.LogInformation($"MN ---- {message}");
    }
    private void LogError(string message)
    {
      _logger.LogInformation($"MN ---- Error in --- {message}");
    }
  }
}
