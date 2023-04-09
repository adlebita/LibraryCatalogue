using LibraryCatalogue.Domain.Interfaces;

namespace LibraryCatalogue.Domain.Models.Author;

public class Author : IEntity
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public required DateOnly BirthDate { get; set; }

    public static Author Create(string firstName, string lastName, string? middleName, DateOnly birthdate)
    {
        return new Author
        {
            FirstName = firstName,
            LastName = lastName,
            MiddleName = middleName,
            BirthDate = birthdate
        };
    }
}