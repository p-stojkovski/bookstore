using Bookstore.Users.Endpoints.CartEndpoints;

namespace Bookstore.Users.CartEndpoints;

internal class CartResponse
{
    public List<CartItemDto> CartItems { get; set; } = new();
}
