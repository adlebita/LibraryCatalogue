using LibraryCatalogue.Domain.Models.Author;
using LibraryCatalogue.Domain.Models.Print;
using Microsoft.EntityFrameworkCore;

namespace LibraryCatalogue.Infrastucture.Database;

public class PrintContext : DbContext
{
    public required DbSet<Print> Prints { get; set; }

    public required DbSet<Author> Authors { get; set; }

    public PrintContext(DbContextOptions<PrintContext> options) : base(options)
    {
    }
    
}