namespace Cmentarz.Mappers.Grave;

using Dto;
using Models;

public class GraveMapper : IGraveMapper
{
    public GraveReadDto MapToDto(Grave grave)
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

    public Grave MapFromDto(GraveReadDto graveDto)
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