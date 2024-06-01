using Ardalis.Result;

namespace Bookstore.OrderProcessing.Integrations;

internal interface IOrderAddressCache
{
    Task<Result<OrderAddress>> GetByIdAsync(Guid addressId);
}
