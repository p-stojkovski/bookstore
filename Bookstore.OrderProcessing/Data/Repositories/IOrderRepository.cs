using Bookstore.OrderProcessing.Entities;

namespace Bookstore.OrderProcessing.Data.Repositories;

internal interface IOrderRepository
{
    Task<List<Order>> ListAsync();
    Task AddAsync(Order order);
    Task SaveChangesAsync();
}
