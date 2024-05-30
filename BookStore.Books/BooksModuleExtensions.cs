using Bookstore.Books.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Bookstore.Books;

public static class BooksModuleExtensions
{
    public static IServiceCollection AddBooksModuleServices(
        this IServiceCollection services,
        ConfigurationManager config, 
        ILogger logger)
    {
        string? connectionString = config.GetConnectionString("BooksConnectionString");
        services.AddDbContext<BookDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookService, BookService>();

        logger.Information("{Module} module services registered.", "Books");

        return services;
    }
}
