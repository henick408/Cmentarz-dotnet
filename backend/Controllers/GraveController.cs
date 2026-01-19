using System.Security.Claims;
using Cmentarz.Data;
using Cmentarz.Dto.Deceased;
using Cmentarz.Dto.Grave;
using Cmentarz.Mappers.Grave;
using Cmentarz.Models;
using Cmentarz.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cmentarz.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GraveController(
    GraveyardDbContext context,
    IGraveMapper graveMapper,
    IGraveService graveService
    ) : ControllerBase
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
                .Include(grave => grave.Deceased)
                .ToListAsync();
        }
        else
        {
            graves = await context.Graves
                .Where(grave => grave.OwnerId == null)
                .Include(grave => grave.Deceased)
                .ToListAsync();
        }

        var graveDtos = graves.Select(graveMapper.MapToReadDto);
        
        return Ok(graveDtos);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "User, Employee")]
    public async Task<IActionResult> Get(int id)
    {
        var grave = await context.Graves
            .Include(grave => grave.Deceased)
            .FirstOrDefaultAsync(grave => grave.Id == id);

        if (grave == null)
        {
            return NotFound();
        }

        var role = User.FindFirstValue(ClaimTypes.Role);

        if (role == "Employee")
        { 
            var graveDto = graveMapper.MapToReadDto(grave);
            return Ok(graveDto);
        }
        
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (grave.OwnerId != userId)
        {
            return Forbid();
        }

        var newGraveDto = graveMapper.MapToReadDto(grave);
        return Ok(newGraveDto);


    }

    [HttpPost]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> Create([FromBody] GraveCreateDto graveDto)
    {
        
        var availableStatus = await context.GraveStatuses.FirstOrDefaultAsync(status => status.Name == "Available");

        if (await context.Graves.FirstOrDefaultAsync(grave => grave.Location == graveDto.Location) != null)
        {
            return Conflict();
        }
        
        var grave = new Grave
        {
            Location = graveDto.Location,
            Price = graveDto.Price,
            StatusId = availableStatus.Id
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
            return NotFound("Grave with given id does not exist");
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
            return NotFound("Grave with given id does not exist");
        }

        context.Graves.Remove(grave);
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{id:int}/reserve")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> ReserveGrave(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        try
        {
            await graveService.ReserveAsync(id, userId);
            return Ok("Grave reserved successfully");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("bury")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Bury([FromBody] DeceasedCreateDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        try
        {
            await graveService.BuryAsync(dto, userId);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok("Burial completed successfully");
    }

    [HttpGet("my-graves")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetMyGraves()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var graves = await context.Graves
            .Where(grave => grave.OwnerId == userId)
            .Include(grave => grave.Deceased)
            .ToListAsync();
        
        var graveDtos = graves.Select(graveMapper.MapToReadDto);
        
        return Ok(graveDtos);
    }
}