using System.ComponentModel.DataAnnotations;

namespace LibraryCatalogue.Domain.Interfaces;

public interface IEntity
{
    [Key]
    Guid Id { get; set; }
}