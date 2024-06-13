using Ardalis.Result;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Bookstore.EmailSending.EmailBackgroundService;

internal interface IReadEmailsFromOutboxService
{
    Task<Result<EmailOutboxEntity>> GetUnprocessedEmailEntityAsync();
}

internal interface IWriteEmailsToOutboxService
{
    Task<UpdateResult?> UpdateEmailEntityDateTimeUtcProcessedAsync(Guid emailEntityId, DateTime dateTime);
}

internal class ReadEmailsFromOutboxService : IReadEmailsFromOutboxService
{
    private readonly IMongoCollection<EmailOutboxEntity> _emailCollection;

    public ReadEmailsFromOutboxService(IMongoCollection<EmailOutboxEntity> emailCollection)
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
}

internal class WriteEmailsToOutboxService : IWriteEmailsToOutboxService
{
    private readonly IMongoCollection<EmailOutboxEntity> _emailCollection;

    public WriteEmailsToOutboxService(IMongoCollection<EmailOutboxEntity> emailCollection)
    {
        _emailCollection = emailCollection;
    }

    public async Task<UpdateResult?> UpdateEmailEntityDateTimeUtcProcessedAsync(Guid emailEntityId, DateTime dateTime)
    {
        var updateFilter = Builders<EmailOutboxEntity>.Filter.Eq(x => x.Id, emailEntityId);

        var update = Builders<EmailOutboxEntity>.Update.Set("DateTimeUtcProcessed", dateTime);

        return await _emailCollection.UpdateOneAsync(updateFilter, update);
    }
}

internal class SendEmailsFromOutboxService : ISendEmailsFromOutboxService
{
    private readonly IReadEmailsFromOutboxService _readOutboxService;
    private readonly IWriteEmailsToOutboxService _writeOutboxService;
    private readonly ISendEmail _emailSender;
    private readonly ILogger<SendEmailsFromOutboxService> _logger;

    public SendEmailsFromOutboxService(IReadEmailsFromOutboxService readOutboxService,
        IWriteEmailsToOutboxService writeOutboxService,
        ISendEmail emailSender,
        ILogger<SendEmailsFromOutboxService> logger)
    {
        _readOutboxService = readOutboxService;
        _writeOutboxService = writeOutboxService;
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task CheckForAndSendEmailsAsync()
    {
        try
        {
            var result = await _readOutboxService.GetUnprocessedEmailEntityAsync();

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
                = await _writeOutboxService.UpdateEmailEntityDateTimeUtcProcessedAsync(emailEntity.Id, DateTime.UtcNow);

            _logger.LogInformation("Processed {result} email records.", updateResult!.ModifiedCount);
        }
        finally
        {
            _logger.LogInformation("Sleeping...");
        }
    }
}
