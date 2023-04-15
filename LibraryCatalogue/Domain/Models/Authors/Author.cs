using LibraryCatalogue.Domain.Interfaces;
using LibraryCatalogue.Domain.Models.Publications;

namespace LibraryCatalogue.Domain.Models.Authors;

public class Author : IEntity
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public required DateOnly BirthDate { get; set; }
    public ICollection<Publication> Publications { get; set; } = new List<Publication>();

    public static Author Create(string firstName, string lastName, string? middleName, DateOnly birthdate)
    {
        return new()
        {
            FirstName = firstName,
            LastName = lastName,
            MiddleName = middleName,
            BirthDate = birthdate
        };
    }

    public void AddPublication(Publication publication)
    {
        Publications.Add(publication);
    }
}