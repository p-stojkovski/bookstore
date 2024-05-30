using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Bookstore.Users.UseCases;
using Bookstore.Users.UsersEndpoints;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Users.CartEndpoints;

internal class AddItem : Endpoint<AddCartItemRequest>
{
    private readonly IMediator _mediator;

    public AddItem(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/cart");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(AddCartItemRequest request,
        CancellationToken cancellationToken = default)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");
        if (emailAddress is null)
        {
            await SendUnauthorizedAsync();
            return;
        }

        var command = new AddItemToCartCommand(request.BookId, request.Quantity, emailAddress);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.Status is ResultStatus.Unauthorized)
        {
            await SendUnauthorizedAsync();
            return;
        }

        await SendOkAsync();
    }
}

