namespace Bookstore.Books;

internal class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<BookDto>> ListBooksAsync()
    {
        var books = (await _bookRepository.ListAllAsync())
            .Select(x => new BookDto(x.Id, x.Title, x.Author, x.Price))
            .ToList();

        return books;
    }
}
