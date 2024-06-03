using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Bookstore.OrderProcessing.OrderEndpoints;
internal class FlushCache : EndpointWithoutRequest
{
    private readonly IDatabase _database;
    private readonly ILogger<FlushCache> _logger;

    public FlushCache(ILogger<FlushCache> logger)
    {
        //TODO: use DI
        var redis = ConnectionMultiplexer.Connect("localhost");
        _database = redis.GetDatabase();
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/flush-cache");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken = default)
    {
        await _database.ExecuteAsync("FLUSHDB");

        _logger.LogInformation("Flushed cache for {db}", "REDIS");
    }
}
