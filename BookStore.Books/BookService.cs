namespace Bookstore.Books;

internal class BookService : IBookService
{
    public IEnumerable<BookDto> ListBooks()
    {
        return [new BookDto(Guid.NewGuid(), "The Fellowship of the Ring", "J.R.R. Tolkien")];
    }
}
