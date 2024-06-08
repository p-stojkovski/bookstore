using System.Reflection;
using Bookstore.OrderProcessing.Domain;
using Bookstore.SharedKernel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.OrderProcessing.Infrastructure.Data;

internal class OrderProcessingDbContext : DbContext
{
    private readonly IDomainEventDispatcher? _dispatcher;

    public OrderProcessingDbContext(DbContextOptions<OrderProcessingDbContext> options,
        IDomainEventDispatcher? dispatcher) : base(options)
    {
        _dispatcher = dispatcher;
    }

    internal DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("OrderProcessing");

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
