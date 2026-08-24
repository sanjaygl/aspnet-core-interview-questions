using API.Database.Entities;

namespace API.Repositories.Orders;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
}
