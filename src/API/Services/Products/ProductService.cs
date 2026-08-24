using API.Models;
using API.Repositories;

namespace API.Services.Product;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAvailableProductsAsync()
    {
        var products = await _productRepository.GetAvailableProductsAsync();
        return products.Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity));
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var p = await _productRepository.GetByIdAsync(id);
        return p == null ? null : new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity);
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
    {
        var product = new Database.Entities.Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity
        };

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return new ProductDto(product.Id, product.Name, product.Description, product.Price, product.StockQuantity);
    }
}
