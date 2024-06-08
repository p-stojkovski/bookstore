using Ardalis.Result;
using Bookstore.Users.Endpoints.UsersEndpoints;
using Bookstore.Users.Interfaces;
using MediatR;

namespace Bookstore.Users.UseCases.User.ListAddresses;

internal class ListAddressesQueryHandler : IRequestHandler<ListAddressesQuery, Result<List<UserAddressDto>>>
{
    private readonly IApplicationUserRepository _userRepository;

    public ListAddressesQueryHandler(IApplicationUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<List<UserAddressDto>>> Handle(ListAddressesQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithAddressesByEmailAsync(request.EmailAddress);
        if (user is null)
        {
            return Result.Unauthorized();
        }

        return user.Addresses
            .Select(x => new UserAddressDto(x.Id,
                x.StreetAddress.Street1,
                x.StreetAddress.Street2,
                x.StreetAddress.State,
                x.StreetAddress.City,
                x.StreetAddress.PostalCode,
                x.StreetAddress.Country))
            .ToList();
    }
}
