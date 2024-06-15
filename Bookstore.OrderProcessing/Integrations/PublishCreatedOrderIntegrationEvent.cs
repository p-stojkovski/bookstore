using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.OrderProcessing.Contracts;
using Bookstore.OrderProcessing.Domain;
using MediatR;

namespace Bookstore.OrderProcessing.Integrations;
internal class PublishCreatedOrderIntegrationEvent : INotificationHandler<OrderCreatedEvent>
{
    private readonly IMediator _mediator;

    public PublishCreatedOrderIntegrationEvent(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        var dto = new OrderDetailsDto
        {
            DateCreated = notification.Order.DateCreated,
            OrderId = notification.Order.Id,
            UserId = notification.Order.UserId,
            OrderItems = notification.Order.OrderItems
            .Select(x => new OrderItemDetails(x.BookId, x.Quantity, x.UnitPrice, x.Description))
            .ToList()
        };

        var integrationEvent = new OrderCreatedIntegrationEvent(dto);

        await _mediator.Publish(integrationEvent);
    }
}
