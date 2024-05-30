namespace Bookstore.Books.Endpoints;

internal class ListBooksResponse
{
    public List<BookDto> Books { get; set; } = new List<BookDto>();
}
