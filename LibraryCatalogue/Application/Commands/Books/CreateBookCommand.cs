using LibraryCatalogue.Domain.Enums;
using LibraryCatalogue.Domain.Models.Author;
using LibraryCatalogue.Domain.Models.Print;
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
            var book = Book.Create(request.Title, request.Author, request.Description, request.Genre);

            await _libraryContext.Prints.AddAsync(book, cancellationToken);
            await _libraryContext.SaveChangesAsync(cancellationToken);

            return book.Id;
        }
    }
}