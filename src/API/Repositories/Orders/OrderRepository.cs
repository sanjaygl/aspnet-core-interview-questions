using API.Database;
using API.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Orders;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(DemoDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
    {
        return await _dbSet
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }
}
