using LibraryCatalogue.Domain.Enums;
using LibraryCatalogue.Domain.Models.Authors;

namespace LibraryCatalogue.Domain.Models.Publications;

public class Magazine : Publication
{
    public static Magazine Create(string title, Author author, string description, Genre genre)
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