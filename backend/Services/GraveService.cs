using Cmentarz.Data;
using Cmentarz.Dto.Deceased;
using Cmentarz.Mappers.Deceased;
using Microsoft.EntityFrameworkCore;

namespace Cmentarz.Services;

public class GraveService(GraveyardDbContext context, IDeceasedMapper deceasedMapper) : IGraveService
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

    public async Task BuryAsync(DeceasedCreateDto dto, int userId)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();
        
        var grave = await context.Graves
            .Include(g => g.Deceased)
            .Include(g => g.Status)
            .FirstOrDefaultAsync(g => g.Id == dto.GraveId);
        
        if (grave == null)
            throw new InvalidOperationException("Grave not found");

        if (grave.OwnerId != userId)
            throw new InvalidOperationException("This grave does not belong to you");

        if (grave.Deceased != null)
            throw new InvalidOperationException("Grave already has a deceased");

        if (grave.Status?.Name != "Reserved")
            throw new InvalidOperationException("Grave is not reserved");

        var deceased = deceasedMapper.MapFromCreateDto(dto);

        context.Deceaseds.Add(deceased);
        grave.Deceased = deceased;
        
        var occupiedStatusId = await context.GraveStatuses
            .Where(s => s.Name == "Occupied")
            .Select(s => s.Id)
            .FirstAsync();

        grave.StatusId = occupiedStatusId;

        await context.SaveChangesAsync();
        await transaction.CommitAsync();
        

    }
}