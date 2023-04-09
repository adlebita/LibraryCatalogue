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
        private readonly PrintContext _printContext;

        public Handler(PrintContext printContext)
        {
            _printContext = printContext;
        }
        
        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = Book.Create(request.Title, request.Author, request.Description, request.Genre);

            await _printContext.AddAsync(book, cancellationToken);

            return book.Id;
        }
    }
}