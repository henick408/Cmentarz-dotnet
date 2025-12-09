namespace Cmentarz.Mappers.Deceased;

using Models;
using Dto.Deceased;

public class DeceasedMapper : IDeceasedMapper
{
    public Deceased MapFromReadDto(DeceasedReadDto deceasedDto)
    {
        return new Deceased
        {
            Id = deceasedDto.Id,
            FirstName = deceasedDto.FirstName,
            LastName = deceasedDto.LastName,
            BirthDate = deceasedDto.BirthDate,
            DeathDate = deceasedDto.DeathDate,
            GraveId = deceasedDto.GraveId
        };
    }

    public DeceasedReadDto MapToReadDto(Deceased deceased)
    {
        return new DeceasedReadDto
        {
            Id = deceased.Id,
            FirstName = deceased.FirstName,
            LastName = deceased.LastName,
            BirthDate = deceased.BirthDate,
            DeathDate = deceased.DeathDate,
            GraveId = deceased.GraveId
        };
    }
}