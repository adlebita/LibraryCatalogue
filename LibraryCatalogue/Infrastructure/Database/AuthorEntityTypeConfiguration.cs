using LibraryCatalogue.Domain.Models.Authors;
using LibraryCatalogue.Domain.Models.Publications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryCatalogue.Infrastructure.Database;

public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasMany<Publication>()
            .WithMany(p => p.Authors)
            .UsingEntity<string>("AuthorPublication");
    }
}