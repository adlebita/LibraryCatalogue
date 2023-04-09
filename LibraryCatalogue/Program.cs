using LibraryCatalogue.Application.Dtos.Requests;
using LibraryCatalogue.Infrastucture.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblyContaining(typeof(Program));
});

builder.Services.AddDbContext<PrintContext>(options => options.UseInMemoryDatabase(nameof(PrintContext)));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/catalogue", () => "List of Books");

app.MapPost("/book", async (CreateBookRequestDto createBookRequestDto) =>
{
    
});

app.Run();