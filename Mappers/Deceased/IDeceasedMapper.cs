namespace Cmentarz.Mappers.Deceased;

using Dto.Deceased;
using Models;

public interface IDeceasedMapper
{
    Deceased MapFromReadDto(DeceasedReadDto deceasedDto);
    DeceasedReadDto MapToReadDto(Deceased deceased);

    Deceased MapFromCreateDto(DeceasedCreateDto deceasedDto);
    DeceasedCreateDto MapToCreateDto(Deceased deceased);
}