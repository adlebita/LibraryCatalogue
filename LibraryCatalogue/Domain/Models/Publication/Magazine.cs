using LibraryCatalogue.Domain.Enums;

namespace LibraryCatalogue.Domain.Models.Publication;

public class Magazine : Publication
{
    public static Magazine Create(string title, Author.Author author, string description, Genre genre)
    {
        var magazine = new Magazine
        {
            Title = title,
            Description = description,
            Genre = genre,
        };

        return magazine;
    }
}