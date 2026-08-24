using API.Database.Entities;
using API.Models;
using API.Repositories.Orders;
using API.Repositories.Users;
using API.Services.Orders.Models;

namespace API.Services.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;

    public OrderService(IOrderRepository orderRepository, IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
    }

    public async Task<OrderResponse> CreateOrderAsync(CreateOrderDto dto, string username)
    {
        if (dto.TotalAmount <= 0)
        {
            return new OrderResponse(false, "Total amount must be greater than zero.");
        }

        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null)
        {
            return new OrderResponse(false, "User account records missing.");
        }

        var newOrder = new Order
        {
            TotalAmount = dto.TotalAmount,
            UserId = user.Id,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        await _orderRepository.AddAsync(newOrder);
        await _orderRepository.SaveChangesAsync();

        return new OrderResponse(true, "Order created successfully!", newOrder);
    }

    public async Task<IEnumerable<Order>> GetUserOrdersAsync(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null)
        {
            return Enumerable.Empty<Order>();
        }

        return await _orderRepository.GetOrdersByUserIdAsync(user.Id);
    }
}
