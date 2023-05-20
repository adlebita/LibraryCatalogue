using System.Net;
using System.Net.Http.Json;
using LibraryCatalogue.Application.Dtos.Requests;
using LibraryCatalogue.Application.Dtos.Requests.Models;
using LibraryCatalogue.Domain.Enums;
using LibraryCatalogue.Domain.Models.Publications;
using LibraryCatalogue.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
#pragma warning disable CS8618

namespace LibraryCatalogue.Integration.Tests.Books;

public class Created : IAsyncLifetime
{
    private WebApplicationFactory<Program> _webHost;
    private LibraryContext _libraryContext;
    private HttpResponseMessage _response;
    private Book _book;

    public async Task InitializeAsync()
    {
        _webHost = new WebApplicationFactory<Program>();
        
        var scopedServices = _webHost.Services.CreateAsyncScope();

        _libraryContext = scopedServices.ServiceProvider.GetRequiredService<LibraryContext>();
        await _libraryContext.Database.EnsureCreatedAsync();

        var createBookRequest = new CreateBookRequestDto
        {
            Author = new AuthorDto("Star", "Man", new DateOnly(1990, 01, 01)),
            Title = "A Star Man's Journey",
            Description = "A journey through space.",
            Genre = Genre.Fiction.ToString()
        };
        
        _response = await _webHost
            .CreateClient()
            .PostAsJsonAsync("books", createBookRequest);

        _book = await _libraryContext.Books.Include(b => b.Authors).FirstAsync();

        await _libraryContext.DisposeAsync();
    }

    [Fact]
    public void Should_BeCreated()
    {
        Assert.Equal(HttpStatusCode.Created, _response.StatusCode);
        Assert.Equal("A Star Man's Journey", _book.Title);
        Assert.Equal("Star", _book.Authors.First().FirstName);
    }

    public async Task DisposeAsync()
    {
        _response.Dispose();
        await _webHost.DisposeAsync();
    }
}