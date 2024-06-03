using Ardalis.Result;
using Bookstore.OrderProcessing.Contracts;
using Bookstore.Users.Interfaces;
using MediatR;

namespace Bookstore.Users.UseCases.Cart.Checkout;

internal class CheckoutCartCommandHandler : IRequestHandler<CheckoutCartCommand, Result<Guid>>
{
    private readonly IApplicationUserRepository _userRepository;
    private readonly IMediator _mediator;

    public CheckoutCartCommandHandler(IApplicationUserRepository userRepository, IMediator mediator)
    {
        _userRepository = userRepository;
        _mediator = mediator;
    }

    public async Task<Result<Guid>> Handle(CheckoutCartCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);
        if (user is null)
        {
            return Result.Unauthorized();
        }

        var cartItems = user.CartItems
            .Select(x => new OrderItemDetails(x.BookId, x.Quantity, x.UnitPrice, x.Description))
            .ToList();

        var createOrderCommand = new CreateOrderCommand(Guid.Parse(user.Id),
            request.ShippingAddressId,
            request.BillingAddressId,
            cartItems);

        //TODO: Consider replacing with a message-based approach for perf reasons
        var result = await _mediator.Send(createOrderCommand, cancellationToken);
        if (!result.IsSuccess)
        {
            return result.Map(x => x.OrderId);
        }

        user.ClearCart();

        await _userRepository.SaveChangesAsync();

        return Result.Success(result.Value.OrderId);
    }
}
