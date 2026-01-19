using Cmentarz.Dto.Deceased;

namespace Cmentarz.Services;

public interface IGraveService
{
    Task ReserveAsync(int graveId, int userId);
    Task BuryAsync(DeceasedCreateDto dto, int userId);
}