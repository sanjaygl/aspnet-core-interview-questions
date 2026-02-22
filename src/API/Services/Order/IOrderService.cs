using API.Services.Order.Models;

namespace API.Services.Order
{
    public interface IOrderService
    {
        Task<Models.Order> CreateOrderAsync(OrderCreateDto orderCreateDto);
        Task<Models.Order?> GetOrderByIdAsync(long orderId);
        Task<IEnumerable<Models.Order>> GetAllOrdersAsync();
        Task UpdateOrderAsync(long orderId, OrderUpdateDto orderUpdateDto);
        Task DeleteOrderAsync(long orderId);
    }
}
