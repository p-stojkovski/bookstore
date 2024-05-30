using Microsoft.EntityFrameworkCore;

namespace Bookstore.Users.Data;

internal class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly UsersDbContext _dbContext;

    public ApplicationUserRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
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
