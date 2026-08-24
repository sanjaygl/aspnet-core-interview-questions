using API.Database.Entities;
using API.Models;
using API.Services.Orders.Models;

namespace API.Services.Orders
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateOrderAsync(CreateOrderDto dto, string username);
        Task<IEnumerable<Order>> GetUserOrdersAsync(string username);
    }
}
