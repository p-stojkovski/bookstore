using Ardalis.Result;
using MediatR;

namespace Bookstore.OrderProcessing.Contracts;

public record CreateOrderCommand(Guid UserId,
    Guid ShippingAddressId,
    Guid BillingAddressId,
    List<OrderItemDetails> OrderItems) : IRequest<Result<OrderDetailsResponse>>;
