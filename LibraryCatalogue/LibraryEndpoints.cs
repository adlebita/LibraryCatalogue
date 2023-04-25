using LibraryCatalogue.Application.Commands.Books;
using LibraryCatalogue.Application.Dtos.Requests;
using LibraryCatalogue.Application.Mappings;
using LibraryCatalogue.Application.Queries;
using MediatR;

namespace LibraryCatalogue;

public static class LibraryEndpoints
{
    public static void AddBooksEndpoints(this WebApplication app)
    {
        app.MapPost("/books", async (ISender sender, BookMappings bookMapper, CreateBookRequestDto createBookRequestDto) =>
        {
            var createBookCommand = bookMapper.CreateBookRequestToCommand(createBookRequestDto);

            var bookId = await sender.Send(createBookCommand);
            return bookId;
        });

        app.MapGet("/books/{id:guid}", async (ISender sender, BookMappings bookMapper, Guid id) =>
        {
            var book = await sender.Send(new GetBookByIdAsNoTracking(id));
            if (book == null) return Results.NotFound();

            var bookDto = bookMapper.CreateBookToBookDto(book);
            return Results.Ok(bookDto);
        });

        app.MapDelete("/books/{id:guid}", async (ISender sender, Guid id) =>
        {
            await sender.Send(new DeleteBookCommand(id));
            return Results.NoContent();
        });

        app.MapPut("/books/{id:guid}", async (ISender sender, BookMappings bookMapper, Guid id, UpdateBookRequestDto updateBookRequestDto) =>
        {
            var updateBookCommand = bookMapper.UpdateBookRequestToCommand(updateBookRequestDto) with { Id = id };
            await sender.Send(updateBookCommand);
            return Results.NoContent();
        });
    }
}