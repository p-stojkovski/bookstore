using Ardalis.Result;
using Bookstore.OrderProcessing.OrderEndpoints;
using MediatR;

namespace Bookstore.OrderProcessing.UseCases;

internal record ListOrdersForUserQuery(string EmailAddress)
    : IRequest<Result<List<OrderSummary>>>;


internal class ListOrdersForUserQueryHandler
    : IRequestHandler<ListOrdersForUserQuery, Result<List<OrderSummary>>>
{
    private readonly IOrderRepository _orderRepository;

    public ListOrdersForUserQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<List<OrderSummary>>> Handle(ListOrdersForUserQuery request, CancellationToken cancellationToken)
    {
        //look for userId for emailAddress

        //TODO: filter by User
        var orders = await _orderRepository.ListAsync();

        return orders.Select(x => new OrderSummary
        {
            DateCreated = x.DateCreated,
            OrderId = x.Id,
            UserId = x.UserId,
            Total = x.OrderItems.Sum(oi => oi.UnitPrice), //need to Include OrderITems
        }).ToList();
    }
}
