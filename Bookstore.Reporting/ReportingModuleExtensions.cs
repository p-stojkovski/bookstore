using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Bookstore.OrderProcessing;

public static class ReportingModuleExtensions
{
    public static IServiceCollection AddReportingModuleServices(this IServiceCollection services, 
        ConfigurationManager config,
        ILogger logger,
        List<Assembly> mediatRAssemblies)
    {
        //string? connectionString = config.GetConnectionString("OrderProcessingConnectionString");
        //services.AddDbContext<OrderProcessingDbContext>(options =>
        //    options.UseSqlServer(connectionString));

        mediatRAssemblies.Add(typeof(ReportingModuleExtensions).Assembly);

        logger.Information("{Module} module services registered.", "Reporting");

        return services;
    }
}
