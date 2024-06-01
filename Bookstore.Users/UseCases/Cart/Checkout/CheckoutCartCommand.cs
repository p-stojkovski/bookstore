using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Bookstore.Books.Contracts;
using Bookstore.Users.UseCases.Cart.AddItem;
using MediatR;

namespace Bookstore.Users.UseCases.Cart.Checkout;

internal record CheckoutCartCommand(string EmailAddress,
    Guid ShippingAddressId,
    Guid BillingAddressId) : IRequest<Result<Guid>>;
