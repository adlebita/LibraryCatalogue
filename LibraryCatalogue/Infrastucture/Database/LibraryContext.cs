using LibraryCatalogue.Domain.Models.Authors;
using LibraryCatalogue.Domain.Models.Publications;
using Microsoft.EntityFrameworkCore;

namespace LibraryCatalogue.Infrastucture.Database;

public class LibraryContext : DbContext
{
    public required DbSet<Publication> Publications { get; set; }
    public required DbSet<Magazine> Magazines { get; set; }
    public required DbSet<Book> Books { get; set; }
    public required DbSet<Author> Authors { get; set; }

    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
    }
}