using Ardalis.Result;
using Bookstore.Books.Contracts;
using MediatR;

namespace Bookstore.Users.UseCases;

internal class AddItemToCartHandler : IRequestHandler<AddItemToCartCommand, Result>
{
    private readonly IApplicationUserRepository _userRepository;
    private readonly IMediator _mediator;

    public AddItemToCartHandler(IApplicationUserRepository userRepository, IMediator mediator)
    {
        _userRepository = userRepository;
        _mediator = mediator;
    }

    public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);
        if (user is null)
        {
            return Result.Unauthorized();
        }

        //TODO: Get description and price from Books module
        var result = await _mediator.Send(new BookDetailsQuery(request.BookId), cancellationToken);
        if (result.Status is ResultStatus.NotFound)
        {
            return Result.NotFound();
        }

        var newCartItem = GetNewCartItem(request, result.Value);

        user.AddItemToCart(newCartItem);

        await _userRepository.SaveChangesAsync();

        return Result.Success();
    }

    private CartItem GetNewCartItem(AddItemToCartCommand request, BookDetailsResponse bookDetails)
        => new CartItem(
            request.BookId,
            $"{bookDetails.Title} by {bookDetails.Author}",
            request.Quantity,
            bookDetails.Price);
}
