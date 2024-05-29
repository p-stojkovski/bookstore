using FastEndpoints;
using FluentValidation;

namespace Bookstore.Books.Endpoints;

public record DeleteBookRequest(Guid Id);

public class DeleteBookRequestValidator : Validator<DeleteBookRequest>
{
    public DeleteBookRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEqual(Guid.Empty)
            .WithMessage("A book id is required.");
    }
}
