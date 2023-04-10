using LibraryCatalogue.Application.Commands.Books;
using LibraryCatalogue.Application.Dtos.Requests;
using Riok.Mapperly.Abstractions;

namespace LibraryCatalogue.Application.Mappings;

[Mapper]
public partial class BookMappings
{
    public partial CreateBookCommand CreateBookRequestToCommand(CreateBookRequestDto createBookRequestDto);
}