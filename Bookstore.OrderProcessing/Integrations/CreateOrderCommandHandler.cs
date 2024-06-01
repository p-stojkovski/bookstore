using Ardalis.Result;
using Bookstore.OrderProcessing.Contracts;
using Bookstore.OrderProcessing.Data.Repositories;
using Bookstore.OrderProcessing.Entities;
using Bookstore.OrderProcessing.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookstore.OrderProcessing.Integrations;
internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDetailsResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, 
        ILogger<CreateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<Result<OrderDetailsResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var items = request.OrderItems
            .Select(x => new OrderItem(x.BookId, x.Quantity, x.UnitPrice, x.Description))
            .ToList();

        var shippingAddress = new Address("123", "", "Skopje", "MK", "1000", "Macedonia");
        var billingAddress = shippingAddress;

        var newOrder = Order.Factory.Create(request.UserId,
            shippingAddress,
            billingAddress,
            items);

        await _orderRepository.AddAsync(newOrder);
        await _orderRepository.SaveChangesAsync();

        _logger.LogInformation("New Order Created! {id}", newOrder.Id);

        return new OrderDetailsResponse(newOrder.Id);
    }
}
