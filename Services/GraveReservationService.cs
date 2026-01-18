using Cmentarz.Data;
using Microsoft.EntityFrameworkCore;

namespace Cmentarz.Services;

public class GraveReservationService(GraveyardDbContext context) : IGraveReservationService
{
    public async Task ReserveAsync(int graveId, int userId)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();

        var grave = await context.Graves
            .FirstOrDefaultAsync(g => g.Id == graveId);

        if (grave == null)
            throw new InvalidOperationException("Grave not found");

        if (grave.OwnerId != null)
            throw new InvalidOperationException("Grave already reserved");

        var reservedStatus = await context.GraveStatuses
            .FirstOrDefaultAsync(s => s.Name == "Reserved");

        if (reservedStatus == null)
            throw new InvalidOperationException("Reserved status not found");

        grave.OwnerId = userId;
        grave.StatusId = reservedStatus.Id;

        await context.SaveChangesAsync();
        await transaction.CommitAsync();
    }
}