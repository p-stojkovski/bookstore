using Ardalis.Result;
using Bookstore.Users.Endpoints.CartEndpoints;
using MediatR;

namespace Bookstore.Users.UseCases.Cart.ListItems;

internal record ListCartItemsQuery(string EmailAddress) : IRequest<Result<List<CartItemDto>>>;
