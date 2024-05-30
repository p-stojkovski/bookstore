using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Users;

internal class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    private readonly List<CartItem> _cartItems = new();
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

    internal void AddItemToCart(CartItem item)
    {
        Guard.Against.Null(item);

        var existingBook = _cartItems.SingleOrDefault(x => x.BookId == item.BookId);
        if (existingBook is not null)
        {
            existingBook.UpdateQuantity(existingBook.Quantity + item.Quantity);

            //TODO: What to do if other details of the item have been updated?

            return;
        }

        _cartItems.Add(item);
    }

}

internal class CartItem
{
    public CartItem()
    { }

    public CartItem(Guid bookId, string description, int quantity, decimal unitPrice)
    {
        BookId = Guard.Against.Default(bookId); ;
        Description = Guard.Against.NullOrEmpty(description);
        Quantity = Guard.Against.Negative(quantity);
        UnitPrice = Guard.Against.Negative(unitPrice);
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid BookId { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    internal void UpdateQuantity(int quantity)
    {
        Quantity = Guard.Against.Negative(quantity);
    }
}

