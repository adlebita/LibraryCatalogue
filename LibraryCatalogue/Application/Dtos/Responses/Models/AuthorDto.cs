namespace LibraryCatalogue.Application.Dtos.Responses.Models;

public record AuthorDto(string FirstName, string LastName, DateOnly BirthDate)
{
    public string? MiddleName { get; set; }
}