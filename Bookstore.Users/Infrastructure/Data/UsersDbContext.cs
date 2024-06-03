using System.Reflection;
using Bookstore.SharedKernel;
using Bookstore.Users.Domain;
using Bookstore.Users.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Users.Infrastructure.Data;

public class UsersDbContext : IdentityDbContext
{
    private readonly IDomainEventDispatcher? _dispatcher;

    public UsersDbContext(DbContextOptions<UsersDbContext> options, IDomainEventDispatcher? dispatcher)
        : base(options)
    {
        _dispatcher = dispatcher;
    }

    internal DbSet<ApplicationUser> ApplicationUsers { get; set; }
    internal DbSet<UserStreetAddress> UserStreetAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Users");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(18, 6);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (result == 0)
        {
            return result;
        }

        if (_dispatcher is null) return result;

        var entitiesWithEvents = ChangeTracker.Entries<IDomainEvents>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Any())
            .ToArray();

        await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

        return result;
    }
}
