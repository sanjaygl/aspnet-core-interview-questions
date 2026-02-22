using API.Services.Order.Models;

namespace API.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly List<Models.Order> _orders = new();

        public Task<Models.Order> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            var order = new Models.Order
            {
                OrderId = 1,
                CustomerId = orderCreateDto.CustomerId,
                OrderDate = orderCreateDto.OrderDate,
                TotalAmount = orderCreateDto.TotalAmount
            };

            _orders.Add(order);

            return Task.FromResult(order);
        }

        public Task DeleteOrderAsync(long orderId)
        {
            var existing = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (existing != null) _orders.Remove(existing);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Models.Order>> GetAllOrdersAsync()
        {
            return Task.FromResult<IEnumerable<Models.Order>>(_orders);
        }

        public Task<Models.Order?> GetOrderByIdAsync(long orderId)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            return Task.FromResult(order);
        }

        public Task UpdateOrderAsync(long orderId, OrderUpdateDto orderUpdateDto)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null && orderUpdateDto.OrderStatus != null)
            {
                order.OrderStatus = orderUpdateDto.OrderStatus;
            }

            return Task.CompletedTask;
        }
    }
}
