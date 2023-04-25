using LibraryCatalogue.Infrastructure.Database;
using MediatR;

namespace LibraryCatalogue.Application.Commands.Books;

public record DeleteBookCommand(Guid BookId) : IRequest
{
    private sealed class Handler : IRequestHandler<DeleteBookCommand>
    {
        private readonly LibraryContext _libraryContext;

        public Handler(LibraryContext libraryContext) => _libraryContext = libraryContext;

        public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _libraryContext.Books.FindAsync(request.BookId, cancellationToken);
            
            if (book == null)
            {
                throw new InvalidOperationException("Book does not exist.");
            }

            _libraryContext.Books.Remove(book);
            await _libraryContext.SaveChangesAsync(cancellationToken);
        }
    }
}