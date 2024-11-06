using Mediator;

namespace Acquisition.Api.Domain.Entities;

public abstract class Entity
{
    public List<INotification> DomainEvents { get; } = new();

    protected void AddDomainEvent(INotification eventItem)
    {
        DomainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        DomainEvents.Remove(eventItem);
    }
}