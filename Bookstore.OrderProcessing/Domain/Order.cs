using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;
using Bookstore.SharedKernel;

namespace Bookstore.OrderProcessing.Domain;

internal class Order : IDomainEvents
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UserId { get; private set; }
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;

    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    private readonly List<DomainEventBase> _domainEvents = new();
    [NotMapped]
    public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
    void IDomainEvents.ClearDomainEvents() => _domainEvents.Clear();

    public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.UtcNow;

    private void AddOrderItem(OrderItem item) => _orderItems.Add(item);

    internal class Factory
    {
        public static Order Create(Guid userId,
            Address shippingAddress,
            Address billingAddress,
            IEnumerable<OrderItem> orderItems)
        {
            var order = new Order();
            order.UserId = userId;
            order.ShippingAddress = shippingAddress;
            order.BillingAddress = billingAddress;

            foreach (var item in orderItems)
            {
                order.AddOrderItem(item);
            }

            order.RegisterDomainEvent(new OrderCreatedEvent(order));

            return order;
        }
    }
}
