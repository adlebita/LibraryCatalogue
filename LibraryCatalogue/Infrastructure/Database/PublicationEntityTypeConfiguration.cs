using LibraryCatalogue.Domain.Models.Publications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryCatalogue.Infrastructure.Database;

public class PublicationEntityTypeConfiguration : IEntityTypeConfiguration<Publication>
{
    public void Configure(EntityTypeBuilder<Publication> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasBaseType<Publication>();

        builder.HasDiscriminator<string>("Type")
            .HasValue<Book>("Book")
            .HasValue<Magazine>("Magazine");

        builder.Property(p => p.Genre)
            .HasConversion<string>();
    }
}