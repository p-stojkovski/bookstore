namespace Bookstore.Books.Endpoints;

public record UpdateBookPriceRequest(Guid Id, decimal Price);
