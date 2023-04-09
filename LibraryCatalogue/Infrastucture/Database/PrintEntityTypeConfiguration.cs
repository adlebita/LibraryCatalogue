using LibraryCatalogue.Domain.Models.Print;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryCatalogue.Infrastucture.Database;

public class PrintEntityTypeConfiguration : IEntityTypeConfiguration<Print>
{
    public void Configure(EntityTypeBuilder<Print> builder)
    {
        builder.HasDiscriminator<string>("Type")
            .HasValue<Book>("Book")
            .HasValue<Magazine>("Magazine");

        builder.Property(p => p.Genre)
            .HasConversion<string>();
    }
}