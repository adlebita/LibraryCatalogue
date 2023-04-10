using LibraryCatalogue.Application.Dtos.Requests;
using LibraryCatalogue.Application.Mappings;
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

app.MapGet("/catalogue", () => "List of Books");

app.MapPost("/books", async (ISender sender, CreateBookRequestDto createBookRequestDto) =>
{
    var bookMapper = new BookMappings();
    var createBookCommand = bookMapper.CreateBookRequestToCommand(createBookRequestDto);
    
    var bookId = await sender.Send(createBookCommand);
    return bookId;
});

app.Run();