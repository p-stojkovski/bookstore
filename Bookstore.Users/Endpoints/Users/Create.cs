using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result.AspNetCore;
using Bookstore.Users.Domain;
using Bookstore.Users.UseCases.User.Create;
using Bookstore.Users.UsersEndpoints;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Users.Endpoints.UsersEndpoints;

internal class Create : Endpoint<CreateUserRequest>
{
    private readonly IMediator _mediator;

    public Create(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateUserCommand(request.Email, request.Password);

        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
        {
            await SendResultAsync(result.ToMinimalApiResult());
            return;
        }

        await SendOkAsync();
    }
}
