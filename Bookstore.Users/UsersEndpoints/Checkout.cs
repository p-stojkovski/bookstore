using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Bookstore.Users.UseCases.Cart.Checkout;
using FastEndpoints;
using MediatR;

namespace Bookstore.Users.UsersEndpoints;

internal record CheckoutRequest(Guid ShippingAddressId, Guid BillingAddressId);
internal record CheckoutResponse(Guid NewOrderId);

internal class Checkout : Endpoint<CheckoutRequest, CheckoutResponse>
{
    private readonly IMediator _mediator;

    public Checkout(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/cart/checkout");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CheckoutRequest request, CancellationToken cancellationToken = default)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");
        if (emailAddress is null)
        {
            await SendUnauthorizedAsync();
            return;
        }

        var command = new CheckoutCartCommand(emailAddress,
            request.ShippingAddressId,
            request.BillingAddressId);

        var result = await _mediator.Send(command, cancellationToken);
        if (result.Status is ResultStatus.Unauthorized)
        {
            await SendUnauthorizedAsync();
            return;
        }

        await SendOkAsync(new CheckoutResponse(result.Value));
    }
}

