using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Bookstore.Users.UseCases;
using FastEndpoints;
using MediatR;

namespace Bookstore.Users.CartEndpoints;

internal class ListCartItems : EndpointWithoutRequest<CartResponse>
{
    private readonly IMediator _mediator;

    public ListCartItems(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Get("/cart");
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

        var query = new ListCartItemsQuery(emailAddress);

        var result = await _mediator.Send(query, cancellationToken);

        if (result.Status is ResultStatus.Unauthorized)
        {
            await SendUnauthorizedAsync();
            return;
        }

        var response = new CartResponse()
        {
            CartItems = result.Value
        };

        await SendAsync(response);
    }
}
