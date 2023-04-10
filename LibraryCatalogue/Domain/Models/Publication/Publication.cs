using LibraryCatalogue.Domain.Enums;
using LibraryCatalogue.Domain.Interfaces;

namespace LibraryCatalogue.Domain.Models.Publication;

public abstract class Publication : IEntity
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required Genre Genre { get; set; }

    public ICollection<Author.Author> Authors { get; set; } = new List<Author.Author>();

    public void AddAuthor(Author.Author author)
    {
        Authors.Add(author);
    }
}