using FastEndpoints;

namespace Bookstore.Books.Endpoints;

internal class Create(IBookService bookService) :
    Endpoint<CreateBookRequest, BookDto>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Post("/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateBookRequest request, CancellationToken cancellationToken = default)
    {
        var newBook = new BookDto(request.Id ?? Guid.NewGuid(),
            request.Title,
            request.Author,
            request.Price);

        await _bookService.CreateBookAsync(newBook);
        await SendCreatedAtAsync<Create>(new { newBook.Id }, newBook); ;
    }
}
