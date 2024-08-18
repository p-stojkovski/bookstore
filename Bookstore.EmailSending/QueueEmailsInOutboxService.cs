using Ardalis.Result;
using Bookstore.EmailSending.Integrations;
using MongoDB.Driver;

namespace Bookstore.EmailSending;

internal class QueueEmailsInOutboxService : IQueueEmailsInOutboxService
{
    private readonly IMongoCollection<EmailOutboxEntity> _emailCollection;

    public QueueEmailsInOutboxService(IMongoCollection<EmailOutboxEntity> emailCollection)
    {
        _emailCollection = emailCollection;
    }

    public async Task QueueEmailForSendingAsync(EmailOutboxEntity entity)
    {
        await _emailCollection.InsertOneAsync(entity);
    }
}
