namespace LibraryCatalogue.Application.Dtos.Responses.Models;

public class BookDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Genre { get; set; }
    public List<AuthorDto> Authors { get; set; } = new();
}