using System.Reflection;
using Bookstore.Users.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Bookstore.Users;

public static class UserModuleExtensions
{
    public static IServiceCollection AddUsersModuleServices(this IServiceCollection services, 
        ConfigurationManager config,
        ILogger logger,
        List<Assembly> mediatRAssemblies)
    {
        string? connectionString = config.GetConnectionString("UsersConnectionString");
        services.AddDbContext<UsersDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<UsersDbContext>();

        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();

        mediatRAssemblies.Add(typeof(UserModuleExtensions).Assembly);

        logger.Information("{Module} module services registered.", "Users");

        return services;
    }
}
