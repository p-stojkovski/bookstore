namespace Bookstore.Books.Endpoints;

public class ListBooksResponse
{
    public List<BookDto> Books { get; set; } = new List<BookDto>();
}
