using Bookstore.OrderProcessing.ValueObjects;

namespace Bookstore.OrderProcessing.Integrations;

// This is materialized view's data model
internal record OrderAddress(Guid Id, Address Address);
