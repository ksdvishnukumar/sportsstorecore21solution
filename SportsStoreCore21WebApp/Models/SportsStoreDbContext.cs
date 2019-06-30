using Microsoft.EntityFrameworkCore;
using SportsStoreCore21WebApp.Models.Entities;

namespace SportsStoreCore21WebApp.Models
{
  public class SportsStoreDbContext: DbContext
  {
    public SportsStoreDbContext(DbContextOptions<SportsStoreDbContext> dbContextOptions) : base(dbContextOptions) { }

    public DbSet<Product> Products { get; set; }
  }
}
