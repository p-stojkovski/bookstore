using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Serilog;

namespace Bookstore.EmailSending;

public static class EmailSendingModuleServicesExtensions
{
    public static IServiceCollection AddEmailSendingModuleServices(this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger,
        List<Assembly> mediatRAssemblies)
    {
        // configure MongoDb
        services.Configure<MongoDBSettings>(config.GetSection("MongoDB"));
        services.AddMongoDB(config);

        // Add module services
        services.AddTransient<ISendEmail, EmailSender>();
        services.AddTransient<IOutboxService, OutboxService>();

        // if using MediatR in this module, add any assemblies that contain handlers
        mediatRAssemblies.Add(typeof(EmailSendingModuleServicesExtensions).Assembly);

        logger.Information("{Module} module services registered.", "Email sending");

        return services;
    }

    public static IServiceCollection AddMongoDB(this IServiceCollection services, IConfiguration config)
    {
        // Register MongoDB client as a singleton
        services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            var settings = config.GetSection("MongoDb").Get<MongoDBSettings>();

            return new MongoClient(settings!.ConnectionString);
        });

        // Register MongoDB database as a singleton
        services.AddSingleton(serviceProvider =>
        {
            var settings = config.GetSection("MongoDb").Get<MongoDBSettings>();
            var client = serviceProvider.GetService<IMongoClient>();

            return client!.GetDatabase(settings!.DatabaseName);
        });

        services.AddTransient(serviceProvider =>
        {
            var database = serviceProvider.GetService<IMongoDatabase>();

            return database!.GetCollection<EmailOutboxEntity>("EmailOutboxEntityCollection");
        });

        return services;
    }
}
