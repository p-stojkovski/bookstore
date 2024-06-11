using Ardalis.Result;
using Bookstore.EmailSending.Contracts;
using MediatR;

namespace Bookstore.EmailSending.Integrations;

internal class OutboxSendEmailCommandHandler : IRequestHandler<SendEmailCommand, Result<Guid>>
{
    private readonly IOutboxService _outboxService;

    public OutboxSendEmailCommandHandler(IOutboxService outboxService)
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
