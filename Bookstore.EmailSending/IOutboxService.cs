using Ardalis.Result;
using MongoDB.Driver;

namespace Bookstore.EmailSending;

internal interface IOutboxService
{
    Task QueueEmailForSendingAsync(EmailOutboxEntity entity);
    Task<Result<EmailOutboxEntity>> GetUnprocessedEmailEntityAsync();
    Task<UpdateResult?> UpdateEmailEntityDateTimeUtcProcessed(Guid emailEntityId, DateTime dateTime);
}
