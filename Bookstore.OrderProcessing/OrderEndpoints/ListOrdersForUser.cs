using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Bookstore.OrderProcessing.UseCases;
using FastEndpoints;
using MediatR;

namespace Bookstore.OrderProcessing.OrderEndpoints;

internal class ListOrdersForUser : EndpointWithoutRequest<ListOrdersForUserResponse>
{
    private readonly IMediator _mediator;

    public ListOrdersForUser(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/orders");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken = default)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");
        if (emailAddress is null)
        {
            await SendUnauthorizedAsync();
            return;
        }

        var result = await _mediator.Send(new ListOrdersForUserQuery(emailAddress), cancellationToken);
        if (result.Status == ResultStatus.Unauthorized)
        {
            await SendUnauthorizedAsync();
            return;
        }

        await SendAsync(new ListOrdersForUserResponse
        {
            Orders = (List<OrderSummary>)result.Value.Select(x => new OrderSummary
            {
                DateCreated = x.DateCreated,
                DateShipped = x.DateShipped,
                OrderId = x.OrderId,
                UserId = x.UserId,
                Total = x.Total,
            }).ToList()
        });
    }
}
