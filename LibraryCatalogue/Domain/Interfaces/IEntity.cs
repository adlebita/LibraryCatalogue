using System.ComponentModel.DataAnnotations;

namespace LibraryCatalogue.Domain.Interfaces;

public interface IEntity
{
    Guid Id { get; set; }
}