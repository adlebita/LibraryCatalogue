using LibraryCatalogue.Application.Commands.Books;
using LibraryCatalogue.Application.Dtos.Requests;
using LibraryCatalogue.Application.Mappings;
using LibraryCatalogue.Application.Queries;
using LibraryCatalogue.Infrastructure.Database;
using LibraryCatalogue.Infrastructure.Mediatr.PipelineBehaviour;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders().AddConsole(); 

builder.Services.Scan(scan => scan
    .FromAssemblyOf<Program>()
    .AddClasses(classes => classes.AssignableTo<IMapperly>())
    .AsSelf()
    .WithTransientLifetime());

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblyContaining(typeof(Program));
    configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddDbContext<LibraryContext>(options => options.UseInMemoryDatabase(nameof(LibraryContext)));

var app = builder.Build();

app.UseHttpLogging();

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

app.Run();