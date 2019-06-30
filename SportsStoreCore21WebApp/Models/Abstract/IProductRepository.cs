using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportsStoreCore21WebApp.Models.Entities;

namespace SportsStoreCore21WebApp.Models.Abstract
{
  public interface IProductRepository
  {
    Task<List<Product>> GetAllProductsAsync();
    Task<Product> FindProductByIdAsync(int productId);
    Task<List<Product>> FindProductsByCategoryAsync(string category);
    Task CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int productId);
    void ClearCache();

  }
}
