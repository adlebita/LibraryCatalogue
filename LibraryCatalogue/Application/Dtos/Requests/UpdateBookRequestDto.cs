using LibraryCatalogue.Application.Dtos.Requests.Models;

namespace LibraryCatalogue.Application.Dtos.Requests;

public class UpdateBookRequestDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required AuthorDto Author { get; set; }
    public required string Description { get; set; }
    public required string Genre { get; set; }
}