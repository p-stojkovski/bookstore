using Ardalis.Result;
using MediatR;

namespace Bookstore.Users.Contracts;

public record UserAddressDetailsByIdQuery(Guid AddressId)
    : IRequest<Result<UserAddressDetails>>;


