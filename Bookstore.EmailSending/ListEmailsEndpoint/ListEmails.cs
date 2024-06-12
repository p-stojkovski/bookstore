using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastEndpoints;
using MongoDB.Driver;

namespace Bookstore.EmailSending.ListEmailsEndpoint;

public class ListEmailResponse
{
    public int Count { get; set; }
    public List<EmailOutboxEntity> Emails { get; internal set; } = new();
}

internal class ListEmails : EndpointWithoutRequest<ListEmailResponse>
{
    private readonly IMongoCollection<EmailOutboxEntity> _emailCollection;

    public ListEmails(IMongoCollection<EmailOutboxEntity> emailCollection)
    {
        _emailCollection = emailCollection;
    }

    public override void Configure()
    {
        Get("/emails");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken =  default)
    {
        //TODO: Implement paging
        var filter = Builders<EmailOutboxEntity>.Filter.Empty;
        var emailEntities = await _emailCollection
            .Find(filter)
            .ToListAsync();

        var response = new ListEmailResponse()
        {
            Count = emailEntities.Count,
            Emails = emailEntities //TODO: Use separate DTO
        };

        Response = response;
    }
}
