using Bookstore.Users.Entities;

namespace Bookstore.Users;

internal interface IReadOnlyUserStreetAddressRepository
{
    Task<UserStreetAddress?> GetById(Guid userStreetAddressId);
}
