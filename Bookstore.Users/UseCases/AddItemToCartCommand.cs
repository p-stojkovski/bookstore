using Ardalis.Result;
using MediatR;

namespace Bookstore.Users.UseCases;

internal record AddItemToCartCommand(Guid BookId, int Quantity, string EmailAddress)
    : IRequest<Result>;
