using Ardalis.Result;
using Bookstore.Users.Endpoints.UsersEndpoints;
using MediatR;

namespace Bookstore.Users.UseCases.User.ListAddresses;

internal record ListAddressesQuery(string EmailAddress) : IRequest<Result<List<UserAddressDto>>>;
