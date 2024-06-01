using Microsoft.EntityFrameworkCore;

namespace Bookstore.OrderProcessing;

internal class OrderRepository: IOrderRepository
{
    private readonly OrderProcessingDbContext _dbContext;

    public OrderRepository(OrderProcessingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
    }

    public async Task<List<Order>> ListAsync()
    {
        // insted of directly using .Include into repository, it's better and flexible to use specification
        return await _dbContext.Orders
            .Include(x => x.OrderItems)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
