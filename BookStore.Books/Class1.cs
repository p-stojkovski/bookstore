using Microsoft.AspNetCore.Builder;

namespace Bookstore.Books;

public interface IBookService
{
    IEnumerable<BookDto> ListBooks();
}

public record BookDto(Guid Id, string Title, string Author);

public class BookService : IBookService
{
    public IEnumerable<BookDto> ListBooks()
    {
        return [new BookDto(Guid.NewGuid(), "The Fellowship of the Ring", "J.R.R. Tolkien")];
    }
}

public static class BookEndpoints
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        app.MapGet("/books", (IBookService bookService) =>
        {
            return bookService.ListBooks();
        });
    }
}
