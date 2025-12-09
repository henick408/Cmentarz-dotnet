namespace Cmentarz.Mappers.Deceased;

using Dto.Deceased;
using Models;

public interface IDeceasedMapper
{
    Deceased MapFromReadDto(DeceasedReadDto deceasedDto);
    DeceasedReadDto MapToReadDto(Deceased deceased);
}