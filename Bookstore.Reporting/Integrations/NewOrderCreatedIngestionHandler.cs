using Bookstore.Books.Contracts;
using Bookstore.OrderProcessing.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookstore.Reporting.Integrations;

internal class NewOrderCreatedIngestionHandler : INotificationHandler<OrderCreatedIntegrationEvent>
{
    private readonly ILogger<NewOrderCreatedIngestionHandler> _logger;
    private readonly OrderIngestionService _orderIngestionService;
    private readonly IMediator _mediator;

    public NewOrderCreatedIngestionHandler(ILogger<NewOrderCreatedIngestionHandler> logger,
        OrderIngestionService orderIngestionService,
        IMediator mediator)
    {
        _logger = logger;
        _orderIngestionService = orderIngestionService;
        _mediator = mediator;
    }

    public async Task Handle(OrderCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling order created event to populate reporting database");

        var orderItems = notification.OrderDetailsDto.OrderItems;
        int year = notification.OrderDetailsDto.DateCreated.Year;
        int month = notification.OrderDetailsDto.DateCreated.Month;

        foreach (var item in orderItems)
        {
            //TODO: Implement Materialized View or other cache
            var bookDetailsQuery = new BookDetailsQuery(item.BookId);

            var result = await _mediator.Send(bookDetailsQuery, cancellationToken);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Issue loading book details for {id}", item.BookId);
                continue;
            }

            string author = result.Value.Author;
            string title = result.Value.Title;

            var sale = new BookSale
            {
                Author = author,
                BookId = item.BookId,
                Month = month,
                Title = title,
                Year = year,
                TotalSales = item.Quantity * item.UnitPrice,
                UnitsSold = item.Quantity
            };

            await _orderIngestionService.AddOrdUpdateMonthlyBookSalesAsync(sale);
        }
    }
}
