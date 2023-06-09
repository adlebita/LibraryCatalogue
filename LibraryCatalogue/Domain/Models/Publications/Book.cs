using LibraryCatalogue.Domain.DomainEvents;
using LibraryCatalogue.Domain.Enums;
using LibraryCatalogue.Domain.Models.Authors;

namespace LibraryCatalogue.Domain.Models.Publications;

public class Book : Publication
{
    public static Book Create(string title, string description, Genre genre, Author author)
    {
        var book = new Book
        {
            Title = title,
            Description = description,
            Genre = genre
        };
        book.AddAuthor(author);
        book.RaiseDomainEvent(new BookCreatedEvent(book));
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