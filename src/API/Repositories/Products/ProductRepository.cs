using API.Database;
using API.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Products;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(DemoDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetAvailableProductsAsync()
    {
        return await _dbSet
            .Where(p => p.StockQuantity > 0)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<bool> UpdateStockAsync(int productId, int quantityToReduce)
    {
        var product = await GetByIdAsync(productId);
        if (product == null || product.StockQuantity < quantityToReduce)
        {
            return false;
        }

        product.StockQuantity -= quantityToReduce;
        return true;
    }
}
