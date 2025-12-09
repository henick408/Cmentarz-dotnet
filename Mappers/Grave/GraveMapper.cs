namespace Cmentarz.Mappers.Grave;

using Dto.Grave;
using Models;

public class GraveMapper : IGraveMapper
{
    public GraveReadDto MapToReadDto(Grave grave)
    {
        return new GraveReadDto
        {
            Id = grave.Id,
            Location = grave.Location,
            OwnerId = grave.OwnerId,
            Price = grave.Price,
            StatusId = grave.StatusId
        };
    }

    public Grave MapFromReadDto(GraveReadDto graveDto)
    {
        return new Grave
        {
            Id = graveDto.Id,
            Location = graveDto.Location,
            Price = graveDto.Price,
            StatusId = graveDto.StatusId,
            OwnerId = graveDto.OwnerId
        };
    }
}