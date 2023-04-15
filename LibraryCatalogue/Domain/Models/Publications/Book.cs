using LibraryCatalogue.Domain.Enums;
using LibraryCatalogue.Domain.Models.Authors;

namespace LibraryCatalogue.Domain.Models.Publications;

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

    public void Update(string title, Author author, string description, Genre genre)
    {
        Title = title;
        Description = description;
        Genre = genre;
        
        Authors.Clear();
        Authors.Add(author);
    }
}