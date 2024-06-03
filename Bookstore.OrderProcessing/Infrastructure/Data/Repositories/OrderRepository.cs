using Bookstore.OrderProcessing.Domain;
using Bookstore.OrderProcessing.Infrastructure.Data;
using Bookstore.OrderProcessing.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.OrderProcessing.Infrastructure.Data.Repositories;

internal class OrderRepository : IOrderRepository
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

    public async Task<List<Order>> ListAsync(bool includeOrderItems = false)
    {
        var query = _dbContext.Orders.AsQueryable();

        // insted of directly using .Include into repository, it's better and flexible to use specification
        if (includeOrderItems)
        {
            query = query.Include(x => x.OrderItems);
        }

        return await query.Include(x => x.OrderItems).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
