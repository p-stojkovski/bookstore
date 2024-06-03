using Ardalis.Result;
using Bookstore.OrderProcessing.Infrastructure;

namespace Bookstore.OrderProcessing.Interfaces;

internal interface IOrderAddressCache
{
    Task<Result<OrderAddress>> GetByIdAsync(Guid addressId);
    Task<Result> StoreAsync(OrderAddress orderAddress);
}
