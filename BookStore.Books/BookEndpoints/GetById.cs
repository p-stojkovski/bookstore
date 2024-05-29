using FastEndpoints;

namespace Bookstore.Books.Endpoints;

internal class GetById(IBookService bookService) :
    Endpoint<GetBookByIdRequest, BookDto>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Get("/books/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetBookByIdRequest request, CancellationToken cancellationToken = default)
    {
        var book = await _bookService.GetBookByIdAsync(request.Id);

        if (book is null)
        {
            await SendNotFoundAsync();
            return;
        }

        await SendAsync(book);
    }
}
