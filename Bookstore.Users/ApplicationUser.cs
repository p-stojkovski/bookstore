using System.Net.Sockets;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Users;

internal class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    private readonly List<CartItem> _cartItems = new();
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();


    private readonly List<UserStreetAddress> _addresses = new();
    public IReadOnlyCollection<UserStreetAddress> Addresses => _addresses.AsReadOnly();

    internal void AddItemToCart(CartItem item)
    {
        Guard.Against.Null(item);

        var existingBook = _cartItems.SingleOrDefault(x => x.BookId == item.BookId);
        if (existingBook is not null)
        {
            existingBook.UpdateQuantity(existingBook.Quantity + item.Quantity);
            existingBook.UpdateDescription(item.Description);
            existingBook.UpdateUnitPrice(item.UnitPrice);

            return;
        }

        _cartItems.Add(item);
    }

    internal UserStreetAddress AddAddress(Address address)
    {
        Guard.Against.Null(address);

        var existingAddress = _addresses.SingleOrDefault(x => x.StreetAddress == address);
        if (existingAddress is not null)
        {
            return existingAddress;
        }

        var newAddress = new UserStreetAddress(Id, address);
        _addresses.Add(newAddress);

        return newAddress;
    }

    internal void ClearCart()
    {
        _cartItems.Clear();
    }
}
