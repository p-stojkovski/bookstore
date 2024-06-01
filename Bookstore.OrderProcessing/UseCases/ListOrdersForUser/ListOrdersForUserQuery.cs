using Ardalis.Result;
using Bookstore.OrderProcessing.OrderEndpoints;
using MediatR;

namespace Bookstore.OrderProcessing.UseCases.ListOrdersForUser;

internal record ListOrdersForUserQuery(string EmailAddress)
    : IRequest<Result<List<OrderSummary>>>;
