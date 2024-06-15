using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.OrderProcessing.Contracts;

/// <summary>
/// Basic details of the order
/// TODO: Include address info for geographic specific reports to use
/// </summary>
public class OrderDetailsDto
{
    public DateTimeOffset DateCreated { get; set; }
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public List<OrderItemDetails> OrderItems { get; set; } = new();
}
