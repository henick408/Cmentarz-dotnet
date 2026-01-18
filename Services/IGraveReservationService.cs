namespace Cmentarz.Services;

public interface IGraveReservationService
{
    Task ReserveAsync(int graveId, int userId);
}