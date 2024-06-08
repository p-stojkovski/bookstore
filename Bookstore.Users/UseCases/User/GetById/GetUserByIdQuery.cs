using Ardalis.Result;
using MediatR;

namespace Bookstore.Users.UseCases.User.GetById;
internal record GetUserByIdQuery(Guid UserId) : IRequest<Result<UserDto>>;
