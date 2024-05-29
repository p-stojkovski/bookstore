
namespace Bookstore.Books;

internal class BookRepository : IBookRepository
{
    public Task AddAsync(Book book)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Book book)
    {
        throw new NotImplementedException();
    }

    public Task<Book?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Book>> ListAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
