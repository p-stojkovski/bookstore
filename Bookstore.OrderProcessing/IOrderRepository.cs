namespace Bookstore.OrderProcessing;

internal interface IOrderRepository
{
    Task<List<Order>> ListAsync();
    Task AddAsync(Order order);
    Task SaveChangesAsync();
}
