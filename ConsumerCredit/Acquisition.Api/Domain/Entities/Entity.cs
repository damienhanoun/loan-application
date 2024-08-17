using Mediator;

namespace Acquisition.Api.Domain.Entities;

public abstract class Entity
{
    public List<INotification> DomainEvents { get; private set; } = null!;

    public void AddDomainEvent(INotification eventItem)
    {
        DomainEvents ??= new List<INotification>();
        DomainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        DomainEvents?.Remove(eventItem);
    }
}