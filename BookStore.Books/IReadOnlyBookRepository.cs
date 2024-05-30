namespace Bookstore.Books;

public interface IReadOnlyBookRepository
{
    Task<Book?> GetByIdAsync(Guid id);
    Task<List<Book>> ListAllAsync();
}
