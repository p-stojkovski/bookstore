using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.Books;

public static class BookServiceExtensions
{
    public static IServiceCollection AddBookServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();

        return services;
    }
}
