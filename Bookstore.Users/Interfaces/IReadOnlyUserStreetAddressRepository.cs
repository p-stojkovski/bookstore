using Bookstore.Users.Domain;

namespace Bookstore.Users.Interfaces;

internal interface IReadOnlyUserStreetAddressRepository
{
    Task<UserStreetAddress?> GetById(Guid userStreetAddressId);
}
