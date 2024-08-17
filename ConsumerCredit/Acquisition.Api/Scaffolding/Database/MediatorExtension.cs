﻿using Acquisition.Api.Domain.Entities;
using Acquisition.Api.Scaffolding.Database;
using Mediator;

namespace Acquisition.Api.Scaffolding;

public static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, AcquisitionContext context)
    {
        var domainEntities = context.ChangeTracker.Entries<Entity>().Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());
        var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();
        domainEntities.ToList().ForEach(entity => entity.Entity.DomainEvents.Clear());

        var tasks = domainEvents
            .Select(async domainEvent => { await mediator.Publish(domainEvent); });

        await Task.WhenAll(tasks);
    }
}