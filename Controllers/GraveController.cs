using System.Security.Claims;
using Cmentarz.Data;
using Cmentarz.Dto.Grave;
using Cmentarz.Mappers.Grave;
using Cmentarz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cmentarz.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GraveController(GraveyardDbContext context, IGraveMapper graveMapper) : ControllerBase
{

    [HttpGet]
    [Authorize(Roles = "User, Employee")]
    public async Task<IActionResult> GetAll()
    {
        
        var userRole = User.FindFirstValue(ClaimTypes.Role);
        List<Grave> graves;
        if (userRole == "Employee")
        {
            graves = await context.Graves
                .ToListAsync();
        }
        else
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            graves = await context.Graves.Where(grave => grave.OwnerId == userId).ToListAsync();
        }

        var graveDtos = graves.Select(graveMapper.MapToReadDto);
        
        return Ok(graveDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var grave = await context.Graves
            .FindAsync(id);

        if (grave == null)
        {
            return NotFound();
        }

        var graveDto = graveMapper.MapToReadDto(grave);

        return Ok(graveDto);
    }

    [HttpPost]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> Create([FromBody] GraveCreateDto graveDto)
    {
        
        var reservedStatus = context.GraveStatuses.FirstOrDefaultAsync(status => status.Name == "Reserved").Id;
        
        var grave = new Grave
        {
            Location = graveDto.Location,
            Price = graveDto.Price,
            StatusId = reservedStatus
        };
        
        await context.Graves.AddAsync(grave);
        await context.SaveChangesAsync();

        var outputGraveDto = graveMapper.MapToReadDto(grave);

        return CreatedAtAction(nameof(Get), new { id = outputGraveDto.Id }, outputGraveDto);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> Update(int id, [FromBody] Grave updated)
    {
        var grave = await context.Graves.FindAsync(id);
        if (grave == null)
        {
            return NotFound();
        }

        var statusExists = await context.GraveStatuses.AnyAsync(s => s.Id == updated.StatusId);
        if (!statusExists)
        {
            return BadRequest("Invalid GraveStatusId");
        }

        grave.Location = updated.Location;
        grave.StatusId = updated.StatusId;

        await context.SaveChangesAsync();

        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> Delete(int id)
    {
        var grave = await context.Graves.FindAsync(id);
        if (grave == null)
        {
            return NotFound();
        }

        context.Graves.Remove(grave);
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{id:int}/reserve")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> ReserveGrave(int id)
    {
        var grave = await context.Graves.FirstOrDefaultAsync(grave => grave.Id == id);

        if (grave == null)
        {
            return NotFound("Grave not found");
        }

        if (grave.OwnerId != null)
        {
            return BadRequest("Grave already reserved");
        }

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var reservedStatus = await context.GraveStatuses.FirstOrDefaultAsync(status => status.Name.ToLower().Equals("awaiting"));

        grave.OwnerId = userId;
        grave.StatusId = reservedStatus.Id;

        await context.SaveChangesAsync();

        return Ok("Grave reserved successfully");

    }
}