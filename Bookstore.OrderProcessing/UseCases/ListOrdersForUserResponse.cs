using Bookstore.OrderProcessing.OrderEndpoints;

namespace Bookstore.OrderProcessing.UseCases;

internal class ListOrdersForUserResponse
{
    public List<OrderSummary> Orders { get; set; } = new();
}
