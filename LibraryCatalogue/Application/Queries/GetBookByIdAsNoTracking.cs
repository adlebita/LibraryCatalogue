using FluentValidation;
using LibraryCatalogue.Domain.Models.Publications;
using LibraryCatalogue.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryCatalogue.Application.Queries;

public sealed record GetBookByIdAsNoTracking(Guid Id) : IRequest<Book?>
{
    private sealed class Handler : IRequestHandler<GetBookByIdAsNoTracking, Book?>
    {
        private readonly LibraryContext _libraryContext;
        private readonly IValidator<GetBookByIdAsNoTracking> _validator;

        public Handler(LibraryContext libraryContext, IValidator<GetBookByIdAsNoTracking> validator)
        {
            _libraryContext = libraryContext;
            _validator = validator;
        }

        public async Task<Book?> Handle(GetBookByIdAsNoTracking request, CancellationToken cancellationToken)
        {
            var x = await _validator.ValidateAsync(request, cancellationToken);
            
            return await _libraryContext
                .Publications
                .Include(p => p.Authors)
                .OfType<Book>()
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        }
    }

    public sealed class Validator : AbstractValidator<GetBookByIdAsNoTracking>
    {
        public Validator()
        {
            RuleFor(r => r.Id)
                .NotEmpty();
        }
    }
}