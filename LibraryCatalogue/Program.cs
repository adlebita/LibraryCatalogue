using LibraryCatalogue.Application.Mappings;
using LibraryCatalogue.Application.PipelineBehaviour;
using LibraryCatalogue.Infrastructure.Database;
using LibraryCatalogue.Presentation;
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

app.AddBooksEndpoints();

app.Run();