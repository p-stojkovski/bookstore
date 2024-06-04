using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Bookstore.Users.UseCases.User;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Users.Endpoints.UsersEndpoints;

internal record AddAddressRequest(string Street1,
    string Street2,
    string City,
    string State,
    string PostalCode,
    string Country);

internal sealed class AddAddress : Endpoint<AddAddressRequest>
{
    private readonly IMediator _mediator;

    public AddAddress(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/users/addresses");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(AddAddressRequest request,
        CancellationToken cancellationToken = default)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");
        if (emailAddress is null)
        {
            await SendUnauthorizedAsync();
            return;
        }

        var command = new AddAddressToUserCommand(emailAddress,
            request.Street1,
            request.Street2,
            request.City,
            request.State,
            request.PostalCode,
            request.Country);

        var result = await _mediator.Send(command);
        if (result.Status is ResultStatus.Unauthorized)
        {
            await SendUnauthorizedAsync();
            return;
        }

        await SendOkAsync();
    }
}
