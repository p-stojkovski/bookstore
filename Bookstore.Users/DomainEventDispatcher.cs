﻿using Bookstore.Users.Interfaces;
using MediatR;

namespace Bookstore.Users;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchAndClearEvents(IEnumerable<IDomainEvents> entitiesWithEvents)
    {
        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();

            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent).ConfigureAwait(false);
            }
        }
    }
}
