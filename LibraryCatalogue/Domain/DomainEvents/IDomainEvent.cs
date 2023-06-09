using MediatR;

namespace LibraryCatalogue.Domain.DomainEvents;

public interface IDomainEvent :  INotification
{
    public DateTime OccurredOn { get; init; }
    public Guid EventId { get; init; }
}