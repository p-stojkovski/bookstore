using FastEndpoints;
using FluentValidation;

namespace Bookstore.Books.Endpoints;

internal record DeleteBookRequest(Guid Id);

internal class DeleteBookRequestValidator : Validator<DeleteBookRequest>
{
    public DeleteBookRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEqual(Guid.Empty)
            .WithMessage("A book id is required.");
    }
}
