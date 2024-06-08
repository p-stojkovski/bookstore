using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Bookstore.EmailSending;

public static class EmailSendingModuleServicesExtensions
{
    public static IServiceCollection AddEmailSendingModuleServices(this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger,
        List<Assembly> mediatRAssemblies)
    {
        services.AddTransient<ISendEmail, EmailSender>();

        mediatRAssemblies.Add(typeof(EmailSendingModuleServicesExtensions).Assembly);

        logger.Information("{Module} module services registered.", "Email sending");

        return services;
    }
}
