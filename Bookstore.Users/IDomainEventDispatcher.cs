namespace Bookstore.Users;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<IDomainEvents> entitiesWithEvents);
}
