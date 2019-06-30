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

    public async Task CreateAsync(Product product)
    {
      try
      {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        LogInfo("ProductRepository.CreateAsync");
      }
      catch (Exception ex)
      {
        LogError($"ProductRepository.CreateAsync - {ex.Message}");
        throw;
      }
    }

    public async Task DeleteAsync(int productId)
    {
      try
      {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        LogInfo("ProductRepository.DeleteAsync");
      }
      catch (Exception ex)
      {
        LogError($"ProductRepository.DeleteAsync - {ex.Message}");
        throw;
      }
    }

    public async Task<Product> FindProductByIdAsync(int productId)
    {
      try
      {
        var product = await _context.Products.FirstOrDefaultAsync(p=>p.ProductId == productId);
        LogInfo("ProductRepository.FindProductByIdAsync");
        return product;
      }
      catch (Exception ex)
      {
        LogError($"ProductRepository.FindProductByIdAsync - {ex.Message}");
        throw;
      }
    }

    public async Task<List<Product>> FindProductsByCategoryAsync(string category)
    {
      try
      {
        var productsList = await _context.Products.Where(p=>p.Category == category).ToListAsync();
        LogInfo($"ProductRepository.FindProductsByCategoryAsync - {category}");
        return productsList;
      }
      catch (Exception ex)
      {
        LogError($"ProductRepository.FindProductsByCategoryAsync - Category={category} - {ex.Message}");
        throw;
      }
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
      try
      {
        var productsList = await _context.Products.ToListAsync();
        LogInfo("ProductRepository.GetAllProductsAsync");
        return productsList;
      }
      catch (Exception ex)
      {
        LogError($"ProductRepository.GetAllProductsAsync - {ex.Message}");
        throw;
      }
    }

    public async Task UpdateAsync(Product product)
    {
      try
      {
        _context.Entry<Product>(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        LogInfo("ProductRepository.UpdateAsync");
      }
      catch (Exception ex)
      {
        LogError($"ProductRepository.UpdateAsync - {ex.Message}");
        throw;
      }
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
      _logger.LogError($"MN ---- Error in --- {message}");
    }
  }
}
