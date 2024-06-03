using Bookstore.OrderProcessing.Domain;

namespace Bookstore.OrderProcessing.Interfaces;

internal interface IOrderRepository
{
    Task<List<Order>> ListAsync(bool includeOrderItems = false);
    Task AddAsync(Order order);
    Task SaveChangesAsync();
}
