using Bookstore.SharedKernel;

namespace Bookstore.Users.Domain.Events;

internal sealed class AddressAddedEvent : DomainEventBase
{
    public AddressAddedEvent(UserStreetAddress newAddress)
    {
        NewAddress = newAddress;
    }

    public UserStreetAddress NewAddress { get; }
}
