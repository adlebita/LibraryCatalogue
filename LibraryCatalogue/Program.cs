using LibraryCatalogue.Application.Commands.Books;
using LibraryCatalogue.Application.Dtos.Requests;
using LibraryCatalogue.Application.Mappings;
using LibraryCatalogue.Application.Queries;
using LibraryCatalogue.Infrastucture.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblyContaining(typeof(Program));
});

builder.Services.AddDbContext<LibraryContext>(options => options.UseInMemoryDatabase(nameof(LibraryContext)));

var app = builder.Build();

app.MapPost("/books", async (ISender sender, CreateBookRequestDto createBookRequestDto) =>
{
    var bookMapper = new BookMappings();
    var createBookCommand = bookMapper.CreateBookRequestToCommand(createBookRequestDto);

    var bookId = await sender.Send(createBookCommand);
    return bookId;
});

app.MapGet("/books/{id:guid}", async (ISender sender, Guid id) =>
{
    var book = await sender.Send(new GetBookByIdAsNoTracking(id));
    if (book == null) return Results.NotFound();

    var bookMapper = new BookMappings();
    var bookDto = bookMapper.CreateBookToBookDto(book);
    return Results.Ok(bookDto);
});

app.MapDelete("/books/{id:guid}", async (ISender sender, Guid id) =>
{
    await sender.Send(new DeleteBookCommand(id));
    return Results.NoContent();
});

app.MapPut("/books/{id:guid}", async (ISender sender, Guid id, UpdateBookRequestDto updateBookRequestDto) =>
{
    var bookMapper = new BookMappings();
    var updateBookCommand = bookMapper.UpdateBookRequestToCommand(updateBookRequestDto) with { Id = id };
    await sender.Send(updateBookCommand);
    return Results.NoContent();
});

app.Run();