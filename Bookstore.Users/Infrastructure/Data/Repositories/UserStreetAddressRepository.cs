using Bookstore.Users.Domain;
using Bookstore.Users.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Users.Infrastructure.Data.Repositories;

internal class UserStreetAddressRepository : IReadOnlyUserStreetAddressRepository
{
    private readonly UsersDbContext _dbContext;

    public UserStreetAddressRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserStreetAddress?> GetById(Guid userStreetAddressId)
    {
        return await _dbContext.UserStreetAddresses
            .SingleOrDefaultAsync(x => x.Id == userStreetAddressId);
    }
}
