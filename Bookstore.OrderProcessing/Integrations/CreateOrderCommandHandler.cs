﻿using Ardalis.Result;
using Bookstore.OrderProcessing.Contracts;
using Bookstore.OrderProcessing.Domain;
using Bookstore.OrderProcessing.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookstore.OrderProcessing.Integrations;

internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDetailsResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<CreateOrderCommandHandler> _logger;
    private readonly IOrderAddressCache _addressCache;

    public CreateOrderCommandHandler(IOrderRepository orderRepository,
        ILogger<CreateOrderCommandHandler> logger,
        IOrderAddressCache addressCache)
    {
        _orderRepository = orderRepository;
        _logger = logger;
        _addressCache = addressCache;
    }

    public async Task<Result<OrderDetailsResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var items = request.OrderItems
            .Select(x => new OrderItem(x.BookId, x.Quantity, x.UnitPrice, x.Description))
            .ToList();

        var shippingAddress = await _addressCache.GetByIdAsync(request.ShippingAddressId);
        var billingAddress = await _addressCache.GetByIdAsync(request.BillingAddressId);

        var newOrder = Order.Factory.Create(request.UserId,
            shippingAddress.Value.Address,
            billingAddress.Value.Address,
            items);

        await _orderRepository.AddAsync(newOrder);
        await _orderRepository.SaveChangesAsync();

        _logger.LogInformation("New Order Created! {id}", newOrder.Id);

        return new OrderDetailsResponse(newOrder.Id);
    }
}
