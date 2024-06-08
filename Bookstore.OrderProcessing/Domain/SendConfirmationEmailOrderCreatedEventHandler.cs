using Ardalis.Result;
using Bookstore.EmailSending.Contracts;
using Bookstore.Users.Contracts;
using MediatR;

namespace Bookstore.OrderProcessing.Domain;

internal class SendConfirmationEmailOrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
{
    private readonly IMediator _mediator;

    public SendConfirmationEmailOrderCreatedEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UserDetailsByIdQuery(notification.Order.UserId), cancellationToken);
        if (!result.IsSuccess)
        {
            //TODO: Log the error
            return;
        }

        var sendEmailCommand = new SendEmailCommand
        {
            To = result.Value.EmailAddress,
            From = "noreply@test.com",
            Subject = "Your bookstore purchase!",
            Body = $"You bought {notification.Order.OrderItems.Count} items."
        };

        Guid emailId = await _mediator.Send(sendEmailCommand, cancellationToken);

        //TODO: Do something with emailId
    }
}
