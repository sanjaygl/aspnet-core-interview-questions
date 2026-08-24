using API.Models;

namespace API.Services.Products;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAvailableProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<ProductDto> CreateProductAsync(CreateProductDto dto);
}
