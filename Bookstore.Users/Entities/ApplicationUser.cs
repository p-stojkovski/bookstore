using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Users.Entities;

internal class ApplicationUser : IdentityUser, IDomainEvents
{
    public string FullName { get; set; } = string.Empty;

    private readonly List<CartItem> _cartItems = new();
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();


    private readonly List<UserStreetAddress> _addresses = new();
    public IReadOnlyCollection<UserStreetAddress> Addresses => _addresses.AsReadOnly();

    private readonly List<DomainEventBase> _domainEvents = new();
    [NotMapped]
    public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
    void IDomainEvents.ClearDomainEvents() => _domainEvents.Clear();

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

        var domainEvent = new AddressAddedEvent(newAddress);
        RegisterDomainEvent(domainEvent);

        return newAddress;
    }

    internal void ClearCart()
    {
        _cartItems.Clear();
    }

    public void ClearDomainEvents()
    {
        throw new NotImplementedException();
    }
}
