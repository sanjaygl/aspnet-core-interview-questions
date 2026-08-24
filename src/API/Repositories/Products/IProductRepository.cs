using API.Database.Entities;

namespace API.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<IEnumerable<Product>> GetAvailableProductsAsync();
    Task<bool> UpdateStockAsync(int productId, int quantityToReduce);
}
