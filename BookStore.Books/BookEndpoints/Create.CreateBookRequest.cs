using FastEndpoints;
using FluentValidation;

namespace Bookstore.Books.Endpoints;

public record CreateBookRequest
{
    public Guid? Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Author { get; init; } = string.Empty;
    public decimal Price { get; init; }
}

public class CreateBookRequestValidator : Validator<CreateBookRequest>
{
    public CreateBookRequestValidator()
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
