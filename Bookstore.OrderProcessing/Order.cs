﻿using System.Net.Sockets;

namespace Bookstore.OrderProcessing;

internal class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public  Guid UserId { get; private set; }
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;

    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.UtcNow;

    private void AddOrderItem(OrderItem item) => _orderItems.Add(item);
}
