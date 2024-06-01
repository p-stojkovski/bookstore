using Ardalis.Result;
using MediatR;

namespace Bookstore.Users.UseCases.Cart.AddItem;

internal record AddItemToCartCommand(Guid BookId, int Quantity, string EmailAddress)
    : IRequest<Result>;
