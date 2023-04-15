using LibraryCatalogue.Domain.Enums;
using LibraryCatalogue.Domain.Interfaces;
using LibraryCatalogue.Domain.Models.Authors;

namespace LibraryCatalogue.Domain.Models.Publications;

public abstract class Publication : IEntity
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required Genre Genre { get; set; }

    public ICollection<Author> Authors { get; set; } = new List<Author>();

    public void AddAuthor(Author author)
    {
        Authors.Add(author);
    }
}