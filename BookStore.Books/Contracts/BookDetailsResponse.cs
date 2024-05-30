namespace Bookstore.Books.Contracts;
public record BookDetailsResponse(Guid BookId, string Title, string Author, decimal Price);
