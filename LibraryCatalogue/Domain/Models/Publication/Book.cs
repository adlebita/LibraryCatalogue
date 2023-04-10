using LibraryCatalogue.Domain.Enums;

namespace LibraryCatalogue.Domain.Models.Publication;

public class Book : Publication
{
    public static Book Create(string title, string description, Genre genre)
    {
        var book = new Book
        {
            Title = title,
            Description = description,
            Genre = genre
        };

        return book;
    }
}