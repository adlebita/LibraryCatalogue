using LibraryCatalogue.Domain.Enums;
using LibraryCatalogue.Domain.Interfaces;

namespace LibraryCatalogue.Domain.Models.Print;

public abstract class Print : IEntity
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required Genre Genre { get; set; }
    
    public List<Author.Author> Authors = new();

}