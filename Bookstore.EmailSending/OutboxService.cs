using Ardalis.Result;
using MongoDB.Driver;

namespace Bookstore.EmailSending;

internal class OutboxService : IOutboxService
{
    private readonly IMongoCollection<EmailOutboxEntity> _emailCollection;

    public OutboxService(IMongoCollection<EmailOutboxEntity> emailCollection)
    {
        _emailCollection = emailCollection;
    }

    public async Task<Result<EmailOutboxEntity>> GetUnprocessedEmailEntityAsync()
    {
        var filter = Builders<EmailOutboxEntity>.Filter.Eq(entity => entity.DateTimeUtcProcessed, null);
        var unsentEmailEntity = await _emailCollection
            .Find(filter)
            .FirstOrDefaultAsync();

        if (unsentEmailEntity is null)
        {
            return Result.NotFound();
        }

        return unsentEmailEntity;
    }

    public async Task QueueEmailForSendingAsync(EmailOutboxEntity entity)
    {
        await _emailCollection.InsertOneAsync(entity);
    }

    public async Task<UpdateResult?> UpdateEmailEntityDateTimeUtcProcessed(Guid emailEntityId, DateTime dateTime)
    {
        var updateFilter = Builders<EmailOutboxEntity>.Filter.Eq(x => x.Id, emailEntityId);

        var update = Builders<EmailOutboxEntity>.Update.Set("DateTimeUtcProcessed", dateTime);

        return await _emailCollection.UpdateOneAsync(updateFilter, update);
    }
}
