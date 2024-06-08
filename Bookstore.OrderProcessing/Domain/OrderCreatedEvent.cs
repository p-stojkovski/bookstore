using Bookstore.SharedKernel;

namespace Bookstore.OrderProcessing.Domain;

internal class OrderCreatedEvent : DomainEventBase
{
    public OrderCreatedEvent(Order order)
    {
        Order = order;
    }

    public Order Order { get; }
}
