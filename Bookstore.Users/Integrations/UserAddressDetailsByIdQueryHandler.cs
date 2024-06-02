using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Bookstore.Users.Contracts;
using FastEndpoints;
using MediatR;

namespace Bookstore.Users.Integrations;

internal class UserAddressDetailsByIdQueryHandler : IRequestHandler<UserAddressDetailsByIdQuery, Result<UserAddressDetails>>
{
    private readonly IReadOnlyUserStreetAddressRepository _addressRepository;

    public UserAddressDetailsByIdQueryHandler(IReadOnlyUserStreetAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<Result<UserAddressDetails>> Handle(UserAddressDetailsByIdQuery request,
        CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetById(request.AddressId);
        if (address is null)
        {
            return Result.NotFound();
        }

        Guid userId = Guid.Parse(address.UserId);

        return new UserAddressDetails(userId,
            address.Id,
            address.StreetAddress.Street1,
            address.StreetAddress.Street2,
            address.StreetAddress.City,
            address.StreetAddress.State,
            address.StreetAddress.PostalCode,
            address.StreetAddress.Country); ;
    }
}
