using Bookstore.Books.Endpoints;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;

namespace Bookstore.Books.Tests.Endpoints;

public class BookGetById(Fixture fixture) : TestBase<Fixture>
{
    [Theory]
    [InlineData("a29b0d03-75bd-4590-a0c6-bec4bcc2908e", "The Fellowship of the Ring")]
    public async Task ReturnExpectedBookGivenIdAsync(string bookId, string expectedTitle)
    {
        //Arrange
        Guid id = Guid.Parse(bookId);
        var request = new GetBookByIdRequest(id);

        //Act
        var testResult = await fixture.Client.GETAsync<GetById, GetBookByIdRequest, BookDto>(request);

        //Assert
        testResult.Response.EnsureSuccessStatusCode();
        testResult.Result.Title.Should().Be(expectedTitle);
    }
}
