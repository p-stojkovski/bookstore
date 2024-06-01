using System.Text.Json;
using Ardalis.Result;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Bookstore.OrderProcessing.Integrations;

internal class OrderAddressCache : IOrderAddressCache
{
    private readonly IDatabase _database;
    private readonly ILogger<OrderAddressCache> _logger;

    public OrderAddressCache(ILogger<OrderAddressCache> logger)
    {
        var redis = ConnectionMultiplexer.Connect("localhost"); //TODO: Get server from config
        _database = redis.GetDatabase();
        _logger = logger;
    }

    public async Task<Result<OrderAddress>> GetByIdAsync(Guid id)
    {
        string? fetchedJson = await _database.StringGetAsync(id.ToString());
        if (fetchedJson is null)
        {
            _logger.LogWarning("Address {id} not found in {database}", id, "REDIS");
            return Result.NotFound();
        }

        var address = JsonSerializer.Deserialize<OrderAddress>(fetchedJson);
        if (address is null)
        {
            return Result.NotFound();
        }

        return Result.Success(address);
    }
}
