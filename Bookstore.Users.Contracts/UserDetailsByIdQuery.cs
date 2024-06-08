using Ardalis.Result;
using MediatR;

namespace Bookstore.Users.Contracts;

public record UserDetailsByIdQuery(Guid UserId) : IRequest<Result<UserDetails>>;
