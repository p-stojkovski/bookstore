using Ardalis.Result;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Bookstore.EmailSending;

internal class SendEmailsFromOutboxService : ISendEmailsFromOutboxService
{
    private readonly IOutboxService _outboxService;
    private readonly ISendEmail _emailSender;
    private readonly ILogger<SendEmailsFromOutboxService> _logger;

    public SendEmailsFromOutboxService(IOutboxService outboxService,
        ISendEmail emailSender,
        ILogger<SendEmailsFromOutboxService> logger)
    {
        _outboxService = outboxService;
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task CheckForAndSendEmailsAsync()
    {
        try
        {
            var result = await _outboxService.GetUnprocessedEmailEntityAsync();

            if (result.Status is ResultStatus.NotFound)
            {
                return;
            }

            var emailEntity = result.Value;

            await _emailSender.SendEmailAsync(emailEntity.To,
                emailEntity.From,
                emailEntity.Subject,
                emailEntity.Body);

            var updateResult
                = await _outboxService.UpdateEmailEntityDateTimeUtcProcessed(emailEntity.Id, DateTime.UtcNow);

            _logger.LogInformation("Processed {result} email records.", updateResult!.ModifiedCount);
        }
        finally
        {
            _logger.LogInformation("Sleeping...");
        }
    }
}
