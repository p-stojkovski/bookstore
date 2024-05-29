using FastEndpoints;

namespace Bookstore.Books;

internal class UpdateBookPriceEndpoint(IBookService bookService) : 
    Endpoint<UpdateBookPriceRequest, BookDto>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Post("/books/{Id}/price-history");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateBookPriceRequest request,
        CancellationToken cancellationToken = default)
    {
        //TODO: Handle not found
        await _bookService.UpdateBookPriceAsync(request.Id, request.Price);

        var updatedBook = await _bookService.GetBookByIdAsync(request.Id);

        await SendAsync(updatedBook);
    }
}
