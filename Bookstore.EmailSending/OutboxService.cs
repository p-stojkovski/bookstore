using MongoDB.Driver;

namespace Bookstore.EmailSending;

internal class OutboxService : IOutboxService
{
    private readonly IMongoCollection<EmailOutboxEntity> _collection;

    public OutboxService(IMongoCollection<EmailOutboxEntity> collection)
    {
        _collection = collection;
    }

    public async Task QueueEmailForSendingAsync(EmailOutboxEntity entity)
    {
        await _collection.InsertOneAsync(entity);
    }
}
