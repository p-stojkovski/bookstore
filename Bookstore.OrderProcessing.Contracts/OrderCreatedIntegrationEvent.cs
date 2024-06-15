using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Bookstore.OrderProcessing.Contracts;
public class OrderCreatedIntegrationEvent : INotification
{
    public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.Now;
    public OrderDetailsDto OrderDetailsDto { get; private set; }

    public OrderCreatedIntegrationEvent(OrderDetailsDto orderDetailsDto)
    {
        OrderDetailsDto = orderDetailsDto;
    }
}

