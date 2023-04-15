using LibraryCatalogue.Application.Commands.Books;
using LibraryCatalogue.Application.Dtos.Requests;
using LibraryCatalogue.Application.Dtos.Responses.Models;
using LibraryCatalogue.Domain.Models.Publications;
using Riok.Mapperly.Abstractions;

namespace LibraryCatalogue.Application.Mappings;

[Mapper]
public partial class BookMappings : IMapperly
{
    public partial CreateBookCommand CreateBookRequestToCommand(CreateBookRequestDto createBookRequestDto);
    public partial BookDto CreateBookToBookDto(Book book);

    public partial UpdateBookCommand UpdateBookRequestToCommand(UpdateBookRequestDto updateBookRequestDto);
}