using Bookstore.Users.Domain;

namespace Bookstore.Users.Interfaces;

public interface IDomainEvents
{
    IEnumerable<DomainEventBase> DomainEvents { get; }
    void ClearDomainEvents();
}
