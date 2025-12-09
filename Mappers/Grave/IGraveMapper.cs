namespace Cmentarz.Mappers.Grave;

using Models;
using Dto.Grave;

public interface IGraveMapper
{
    GraveReadDto MapToReadDto(Grave grave);
    Grave MapFromReadDto(GraveReadDto graveDto);
}