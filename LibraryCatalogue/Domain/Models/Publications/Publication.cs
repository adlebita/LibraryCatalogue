﻿using LibraryCatalogue.Domain.Enums;
using LibraryCatalogue.Domain.Models.Authors;

namespace LibraryCatalogue.Domain.Models.Publications;

public abstract class Publication : Entity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required Genre Genre { get; set; }

    public ICollection<Author> Authors { get; set; } = new List<Author>();

    protected void AddAuthor(Author author)
    {
        Authors.Add(author);
    }
}