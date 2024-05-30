namespace Bookstore.Books.Endpoints;

internal record ListBooksResponse
{
    public List<BookDto> Books { get; init; } = new List<BookDto>();
}
