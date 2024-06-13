using Ardalis.Result;
using Bookstore.EmailSending.Contracts;
using MediatR;
using MongoDB.Driver;

namespace Bookstore.EmailSending.Integrations;

internal interface IQueueEmailsInOutboxService
{
    Task QueueEmailForSendingAsync(EmailOutboxEntity entity);
}

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

internal class QueueEmailInOutboxSendEmailCommandHandler : IRequestHandler<SendEmailCommand, Result<Guid>>
{
    private readonly IQueueEmailsInOutboxService _outboxService;

    public QueueEmailInOutboxSendEmailCommandHandler(IQueueEmailsInOutboxService outboxService)
    {
        _outboxService = outboxService;
    }

    public async Task<Result<Guid>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var newEntity = new EmailOutboxEntity
        {
            Body = request.Body,
            Subject = request.Subject,
            To = request.To, 
            From = request.From,
        };

        await _outboxService.QueueEmailForSendingAsync(newEntity);

        return newEntity.Id;
    }
}
