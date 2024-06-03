using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Users.Domain;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Users.UsersEndpoints;

internal class Login : Endpoint<LoginUserRequest>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public Login(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public override void Configure()
    {
        Post("/users/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            await SendUnauthorizedAsync();
            return;
        }

        var loginSuccessful = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!loginSuccessful)
        {
            await SendUnauthorizedAsync();
            return;
        }

        var jwtSecret = Config["Auth:JwtSecret"]!;
        var token = JwtBearer.CreateToken(
            o =>
            {
                o.SigningKey = jwtSecret;
                o.User["EmailAddress"] = user.Email!;
            });

        await SendAsync(token);
    }
}
