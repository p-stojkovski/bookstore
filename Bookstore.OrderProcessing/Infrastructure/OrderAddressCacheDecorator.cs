using Ardalis.Result;
using Bookstore.OrderProcessing.Domain;
using Bookstore.OrderProcessing.Interfaces;
using Bookstore.Users.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookstore.OrderProcessing.Infrastructure;

internal class OrderAddressCacheDecorator : IOrderAddressCache
{
    private readonly OrderAddressCache _orderAddressCache;
    private readonly IMediator _mediator;
    private readonly ILogger<OrderAddressCacheDecorator> _logger;

    public OrderAddressCacheDecorator(OrderAddressCache orderAddressCache,
        IMediator mediator,
        ILogger<OrderAddressCacheDecorator> logger)
    {
        _orderAddressCache = orderAddressCache;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<Result<OrderAddress>> GetByIdAsync(Guid addressId)
    {
        var result = await _orderAddressCache.GetByIdAsync(addressId);
        if (result.IsSuccess)
        {
            return result;
        }

        if (result.Status is ResultStatus.NotFound)
        {
            _logger.LogInformation("Address {id} not found; fetching from source.", addressId);

            var query = new UserAddressDetailsByIdQuery(addressId);

            var queryResult = await _mediator.Send(query);
            if (queryResult.IsSuccess)
            {
                var dto = queryResult.Value;
                var address = new Address(dto.Street1,
                    dto.Street2,
                    dto.City,
                    dto.State,
                    dto.PostalCode,
                    dto.Country);

                var orderAddress = new OrderAddress(dto.AddressId, address);

                await StoreAsync(orderAddress);

                return orderAddress;
            }
        }

        return Result.NotFound();
    }

    public Task<Result> StoreAsync(OrderAddress orderAddress)
    {
        return _orderAddressCache.StoreAsync(orderAddress);
    }
}
