using LibraryCatalogue.Domain.DomainEvents;
using MediatR;

namespace LibraryCatalogue.Domain.DomainEventHandlers;

public record BookCreatedEventHandler : INotificationHandler<BookCreatedEvent>
{
    public Task Handle(BookCreatedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}