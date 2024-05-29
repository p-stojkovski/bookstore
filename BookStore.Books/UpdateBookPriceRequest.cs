namespace Bookstore.Books;

public record UpdateBookPriceRequest(Guid Id, decimal Price);
