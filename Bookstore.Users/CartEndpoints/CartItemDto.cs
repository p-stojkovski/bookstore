namespace Bookstore.Users.CartEndpoints;

internal record CartItemDto(Guid Id, Guid BookId, string Description, 
    int Quantity, decimal UnitPrice);
