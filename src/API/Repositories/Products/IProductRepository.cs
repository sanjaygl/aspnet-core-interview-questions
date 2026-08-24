using API.Database.Entities;

namespace API.Repositories.Products;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<IEnumerable<Product>> GetAvailableProductsAsync();
    Task<bool> UpdateStockAsync(int productId, int quantityToReduce);
}
