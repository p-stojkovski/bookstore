using Bookstore.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Users.Data;

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
            .Include(x => x.StreetAddress)
            .SingleOrDefaultAsync(x => x.Id == userStreetAddressId);
    }
}
