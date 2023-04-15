using LibraryCatalogue.Domain.Enums;
using LibraryCatalogue.Domain.Models.Authors;
using LibraryCatalogue.Domain.Models.Publications;
using LibraryCatalogue.Infrastucture.Database;
using MediatR;

namespace LibraryCatalogue.Application.Commands.Books;

public record CreateBookCommand(string Title, Author Author, string Description, Genre Genre) : IRequest<Guid>
{
    private sealed class Handler : IRequestHandler<CreateBookCommand, Guid>
    {
        private readonly LibraryContext _libraryContext;

        public Handler(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = Book.Create(request.Title, request.Description, request.Genre);
            book.AddAuthor(request.Author);
            request.Author.AddPublication(book);

            await _libraryContext.AddAsync(request.Author, cancellationToken);
            await _libraryContext.AddAsync(book, cancellationToken);
            await _libraryContext.SaveChangesAsync(cancellationToken);

            return book.Id;
        }
    }
}