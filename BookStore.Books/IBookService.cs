namespace Bookstore.Books;

internal interface IBookService
{
    Task<List<BookDto>> ListBooksAsync();
}
