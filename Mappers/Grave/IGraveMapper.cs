namespace Cmentarz.Mappers.Grave;

using Models;
using Dto;

public interface IGraveMapper
{
    GraveReadDto MapToDto(Grave grave);
    Grave MapFromDto(GraveReadDto graveDto);
}