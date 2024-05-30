using Ardalis.Result;
using MediatR;

namespace Bookstore.Books.Contracts;

public record BookDetailsQuery(Guid BookId): IRequest<Result<BookDetailsResponse>>;
