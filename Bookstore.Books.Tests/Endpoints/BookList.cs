using Bookstore.Books.Endpoints;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using Xunit.Abstractions;

namespace Bookstore.Books.Tests.Endpoints;

public class BookList(Fixture fixture) : TestBase<Fixture>
{
    [Fact]
    public async Task ReturnThreeBooksAsync()
    {
        //Act
        var testResult = await fixture.Client.GETAsync<List, ListBooksResponse>();

        //Assert
        testResult.Response.EnsureSuccessStatusCode();
        testResult.Result.Books.Count.Should().Be(1);
    }
}
