using Microsoft.EntityFrameworkCore;

namespace Bookstore.Books.Data;

internal class BookRepository : IBookRepository
{
    private readonly BookDbContext _dbContext;

    public BookRepository(BookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task AddAsync(Book book)
    {
        _dbContext.Books.Add(book);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Book book)
    {
        _dbContext.Remove(book);

        return Task.CompletedTask;
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Books.FindAsync(id);
    }

    public async Task<List<Book>> ListAllAsync()
    {
        return await _dbContext.Books.ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
