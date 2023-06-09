﻿using FluentValidation;
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

        public Handler(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task<Book?> Handle(GetBookByIdAsNoTracking request, CancellationToken cancellationToken)
        {
            return await _libraryContext
                .Publications
                .Include(p => p.Authors)
                .OfType<Book>()
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        }
    }

    public sealed class GetBookByIdAsNoTrackingValidator : AbstractValidator<GetBookByIdAsNoTracking>
    {
        public GetBookByIdAsNoTrackingValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty();
        }
    }
}