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

namespace Bookstore.Users.Endpoints.UsersEndpoints;

public class AddressListResponse
{
    public List<UserAddressDto> Addresses { get; set; } = new();
}

public record UserAddressDto(Guid Id,
    string Street1,
    string Street2,
    string City,
    string State,
    string PostalCode,
    string Country);

internal class ListAddresses : EndpointWithoutRequest<AddressListResponse>
{
    private readonly IMediator _mediator;

    public ListAddresses(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/users/addresses");
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

        var result = await _mediator.Send(new ListAddressesQuery(emailAddress), cancellationToken);
        if (result.Status is ResultStatus.Unauthorized)
        {
            await SendUnauthorizedAsync();
            return;
        }

        await SendAsync(new AddressListResponse
        {
            Addresses = result.Value
        });
    }
}
