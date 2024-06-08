using Ardalis.Result;
using Bookstore.EmailSending.Contracts;
using Bookstore.Users.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Users.UseCases.User.Create;

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMediator _mediator;

    public CreateUserCommandHandler(UserManager<ApplicationUser> userManager, 
        IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }

    public async Task<Result> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var newUser = new ApplicationUser
        {
            Email = command.Email,
            UserName = command.Email
        };

        var result = await _userManager.CreateAsync(newUser, command.Password);
        if (!result.Succeeded)
        {
            return Result.Error(new ErrorList(result.Errors.Select(x => x.Description).ToArray()));
        }

        // send welcome email
        var sendEmailCommand = new SendEmailCommand
        {
            To = command.Email,
            From = "dontreply@test.com",
            Subject = "Welcome to our bookstore!",
            Body = "Thank you for your registration!"
        };

        _ = await _mediator.Send(sendEmailCommand, cancellationToken);

        return Result.Success();
    }
}

