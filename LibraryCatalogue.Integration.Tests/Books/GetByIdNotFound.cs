using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

#pragma warning disable CS8618

namespace LibraryCatalogue.Integration.Tests.Books;

public class GetByIdNotFound : IAsyncLifetime
{
    private WebApplicationFactory<Program> _webHost;
    private HttpResponseMessage _response;

    public async Task InitializeAsync()
    {
        _webHost = new WebApplicationFactory<Program>();

        _response = await _webHost
            .CreateClient()
            .GetAsync($"/books/{Guid.NewGuid()}");
    }

    [Fact]
    public void ShouldBe_NotFound() => Assert.Equal(HttpStatusCode.NotFound, _response.StatusCode);

    public async Task DisposeAsync()
    {
        _response.Dispose();
        await _webHost.DisposeAsync();
    }
}