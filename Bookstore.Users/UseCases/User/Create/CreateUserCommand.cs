using Ardalis.Result;
using MediatR;

namespace Bookstore.Users.UseCases.User.Create;

internal record CreateUserCommand(string Email, string Password) : IRequest<Result>;
