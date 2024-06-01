using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Bookstore.OrderProcessing;

public static class OrderProcessingModuleExtensions
{
    public static IServiceCollection AddOrderProcessingModuleServices(this IServiceCollection services, 
        ConfigurationManager config,
        ILogger logger,
        List<Assembly> mediatRAssemblies)
    {
        string? connectionString = config.GetConnectionString("OrderProcessingConnectionString");
        services.AddDbContext<OrderProcessingDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IOrderRepository, OrderRepository>();

        mediatRAssemblies.Add(typeof(OrderProcessingModuleExtensions).Assembly);

        logger.Information("{Module} module services registered.", "OrderProcessing");

        return services;
    }
}
