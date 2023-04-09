using LibraryCatalogue.Domain.Enums;

namespace LibraryCatalogue.Domain.Models.Print;

public class Book : Print
{
    public static Book Create(string title, Author.Author author, string description, Genre genre)
        => new()
        {
            Title = title,
            Description = description,
            Genre = genre,
            Authors = new List<Author.Author> {author}
        };
}