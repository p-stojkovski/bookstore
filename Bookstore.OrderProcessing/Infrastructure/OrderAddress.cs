using Bookstore.OrderProcessing.Domain;

namespace Bookstore.OrderProcessing.Infrastructure;

// This is materialized view's data model
internal record OrderAddress(Guid Id, Address Address);
