namespace LibraryCatalogue.Application.Dtos.Requests.Models;

public record AuthorDto(string FirstName, string LastName, DateOnly BirthDate)
{
    public string? MiddleName { get; set; }
}