using LibraryCatalogue.Domain.Models.Publications;

namespace LibraryCatalogue.Domain.DomainEvents;

public record BookCreatedEvent(Book Book) : IDomainEvent
{
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
    public Guid EventId { get; init; } = Guid.NewGuid();
}