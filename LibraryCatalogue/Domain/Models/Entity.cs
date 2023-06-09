using LibraryCatalogue.Domain.DomainEvents;
// ReSharper disable CollectionNeverQueried.Local

namespace LibraryCatalogue.Domain.Models;

public abstract class Entity
{
    public Guid Id { get; set; }
    
    private List<IDomainEvent> DomainEvents { get; } = new();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        DomainEvents.Add(domainEvent);
    }
}