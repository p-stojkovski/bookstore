using FastEndpoints;
using FluentValidation;

namespace Bookstore.Books.Endpoints;

internal record UpdateBookPriceRequest(Guid Id, decimal Price);

internal class UpdateBookPriceRequestValidator : Validator<UpdateBookPriceRequest>
{
    public UpdateBookPriceRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEqual(Guid.Empty)
            .WithMessage("A book id is required.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Book prices may not be negative.");
    }
}
