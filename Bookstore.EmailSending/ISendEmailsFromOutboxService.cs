namespace Bookstore.EmailSending;

internal interface ISendEmailsFromOutboxService
{
    Task CheckForAndSendEmailsAsync();
}
