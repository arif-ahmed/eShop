using eShop.Models.Catalog;
using Microsoft.EntityFrameworkCore;

namespace eShop.Data;
public class EShopDbContext : DbContext
{
    public EShopDbContext(DbContextOptions<EShopDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Additional model configurations can be added here
    }

    public DbSet<Category> Categories { get; set; }
}
