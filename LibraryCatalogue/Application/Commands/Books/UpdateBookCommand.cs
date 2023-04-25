using LibraryCatalogue.Domain.Enums;
using LibraryCatalogue.Domain.Models.Authors;
using LibraryCatalogue.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryCatalogue.Application.Commands.Books;

public record UpdateBookCommand(Guid Id, string Title, Author Author, string Description, Genre Genre) : IRequest
{
    private sealed class Handler : IRequestHandler<UpdateBookCommand>
    {
        private readonly LibraryContext _libraryContext;

        public Handler(LibraryContext libraryContext) => _libraryContext = libraryContext;

        public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _libraryContext.Books
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (book == null)
            {
                throw new InvalidOperationException("Book does not exist.");
            }
            book.Update(request.Title, request.Author, request.Description, request.Genre);

            await _libraryContext.SaveChangesAsync(cancellationToken);
        }
    }
}