using Bookstore.Users.Domain;

namespace Bookstore.Users.Interfaces;

internal interface IApplicationUserRepository
{
    Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email);
    Task<ApplicationUser> GetUserWithCartByEmailAsync(string email);
    Task SaveChangesAsync();
}
