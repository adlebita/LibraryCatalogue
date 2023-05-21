using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using LibraryCatalogue.Application.Dtos.Responses.Models;
using LibraryCatalogue.Domain.Enums;
using LibraryCatalogue.Domain.Models.Publications;
using LibraryCatalogue.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable CS8618

namespace LibraryCatalogue.Integration.Tests.Books;

public class GetByIdOk : IAsyncLifetime
{
    private WebApplicationFactory<Program> _webHost;
    private HttpResponseMessage _response;
    private LibraryContext _libraryContext;
    private BookDto? _book;

    public async Task InitializeAsync()
    {
        _webHost = new WebApplicationFactory<Program>();
        
        var scopedServices = _webHost.Services.CreateAsyncScope();

        _libraryContext = scopedServices.ServiceProvider.GetRequiredService<LibraryContext>();
        await _libraryContext.Database.EnsureCreatedAsync();

        var book = Book.Create("Flowers for Algernon", "Ignorance is bliss.", Genre.Fiction);
        await _libraryContext.Books.AddAsync(book);
        await _libraryContext.SaveChangesAsync();
        _libraryContext.ChangeTracker.Clear();
        
        _response = await _webHost
            .CreateClient()
            .GetAsync($"/books/{book.Id}");

        await _libraryContext.DisposeAsync();
        
        _book = await _response.Content.ReadFromJsonAsync(typeof(BookDto)) as BookDto;
    }

    [Fact]
    public void ShouldBe_NotFound()
    {
        Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
        Assert.NotNull(_book);
        Assert.Equal("Flowers for Algernon", _book.Title);
    }

    public async Task DisposeAsync()
    {
        _response.Dispose();
        await _webHost.DisposeAsync();
    }
}