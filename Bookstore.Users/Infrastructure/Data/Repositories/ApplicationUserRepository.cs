using Bookstore.Users.Domain;
using Bookstore.Users.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Users.Infrastructure.Data.Repositories;

internal class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly UsersDbContext _dbContext;

    public ApplicationUserRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApplicationUser> GetUserByIdAsync(Guid userId)
    {
        return await _dbContext.ApplicationUsers
            .SingleAsync(x => x.Id == userId.ToString());
    }

    public async Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email)
    {
        return await _dbContext.ApplicationUsers
           .Include(x => x.Addresses)
           .SingleAsync(x => x.Email == email);
    }

    public async Task<ApplicationUser> GetUserWithCartByEmailAsync(string email)
    {
        return await _dbContext.ApplicationUsers
            .Include(x => x.CartItems)
            .SingleAsync(x => x.Email == email);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
