namespace Bookstore.Books;

internal interface IBookService
{
    IEnumerable<BookDto> ListBooks();
}
