namespace Bookstore.EmailSending;

internal interface IOutboxService
{
    Task QueueEmailForSendingAsync(EmailOutboxEntity entity);
}
