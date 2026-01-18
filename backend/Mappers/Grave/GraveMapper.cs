using Cmentarz.Dto.Deceased;
using Cmentarz.Mappers.Deceased;

namespace Cmentarz.Mappers.Grave;

using Dto.Grave;
using Models;

public class GraveMapper(IDeceasedMapper deceasedMapper) : IGraveMapper
{
    public GraveReadDto MapToReadDto(Grave grave)
    {
        var deceasedDto = grave.Deceased == null ? null : deceasedMapper.MapToReadDto(grave.Deceased);
        return new GraveReadDto
        {
            Id = grave.Id,
            Location = grave.Location,
            OwnerId = grave.OwnerId,
            Price = grave.Price,
            StatusId = grave.StatusId,
            Deceased = deceasedDto
        };
    }

    public Grave MapFromReadDto(GraveReadDto graveDto)
    {
        var deceased = graveDto.Deceased == null ? null : deceasedMapper.MapFromReadDto(graveDto.Deceased);

        return new Grave
        {
            Id = graveDto.Id,
            Location = graveDto.Location,
            Price = graveDto.Price,
            StatusId = graveDto.StatusId,
            OwnerId = graveDto.OwnerId,
            Deceased = deceased
        };
    }
}