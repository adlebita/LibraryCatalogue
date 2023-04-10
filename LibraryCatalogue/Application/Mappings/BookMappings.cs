using LibraryCatalogue.Application.Commands.Books;
using LibraryCatalogue.Application.Dtos.Requests;
using LibraryCatalogue.Application.Dtos.Responses.Models;
using LibraryCatalogue.Domain.Models.Publication;
using Riok.Mapperly.Abstractions;

namespace LibraryCatalogue.Application.Mappings;

[Mapper]
public partial class BookMappings
{
    public partial CreateBookCommand CreateBookRequestToCommand(CreateBookRequestDto createBookRequestDto);
    public partial BookDto CreateBookToBookDto(Book book);
}