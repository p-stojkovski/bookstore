namespace Bookstore.EmailSending;

internal interface ISendEmail
{
    Task SendEmailAsync(string to, string from, string subject, string body);
}
