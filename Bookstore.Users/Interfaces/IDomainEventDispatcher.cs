namespace Bookstore.Users.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<IDomainEvents> entitiesWithEvents);
}
