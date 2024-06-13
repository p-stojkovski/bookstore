namespace Bookstore.EmailSending.EmailBackgroundService;

internal interface ISendEmailsFromOutboxService
{
    Task CheckForAndSendEmailsAsync();
}
