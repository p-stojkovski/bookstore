using Ardalis.Result;
using Bookstore.Users.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Users.UseCases.User.Create;

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public CreateUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var newUser = new ApplicationUser
        {
            Email = command.Email,
            UserName = command.Email
        };

        var result = await _userManager.CreateAsync(newUser, command.Password);
        if (result.Succeeded)
        {
            return Result.Success();
        }

        return Result.Error(new ErrorList(result.Errors.Select(x => x.Description).ToArray()));
    }
}

