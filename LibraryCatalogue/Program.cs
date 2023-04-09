using LibraryCatalogue.Application.Commands.Books;
using LibraryCatalogue.Application.Dtos.Requests;
using LibraryCatalogue.Domain.Enums;
using LibraryCatalogue.Domain.Models.Author;
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

app.MapGet("/", () => "Hello World!");

app.MapGet("/catalogue", () => "List of Books");

app.MapPost("/book", async (ISender sender, CreateBookRequestDto createBookRequestDto) =>
{
    var author = Author.Create(createBookRequestDto.Author.FirstName,
        createBookRequestDto.Author.LastName,
        createBookRequestDto.Author.MiddleName,
        createBookRequestDto.Author.BirthDate);

    var createBookCommand = new CreateBookCommand(
        createBookRequestDto.Title, 
        author, 
        createBookRequestDto.Description,
        Enum.Parse<Genre>(createBookRequestDto.Genre));
    
    var bookId = await sender.Send(createBookCommand);
    return bookId;
});

app.Run();