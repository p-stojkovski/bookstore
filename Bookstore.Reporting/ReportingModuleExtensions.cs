using System.Reflection;
using Bookstore.Reporting;
using Bookstore.Reporting.Integrations;
using Bookstore.Reporting.ReportEndpoints;
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

        // configure module services
        services.AddScoped<ITopSellingBooksReportService, TopSellingBooksReportService>();
        services.AddScoped<ISalesReportService, SalesReportService>();
        services.AddScoped<OrderIngestionService>();

        mediatRAssemblies.Add(typeof(ReportingModuleExtensions).Assembly);

        logger.Information("{Module} module services registered.", "Reporting");

        return services;
    }
}
