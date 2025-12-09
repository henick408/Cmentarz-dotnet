using Cmentarz.Data;
using Cmentarz.Dto;
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
    public async Task<IActionResult> GetAll()
    {
        var graves = await context.Graves
            .ToListAsync();

        return Ok(graves);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var grave = await context.Graves
            .Include(g => g.Status)
            .Include(g => g.Deceased)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (grave == null)
        {
            return NotFound();
        }

        return Ok(grave);
    }

    [HttpPost]
    //[Authorize(Roles = "Employee")]
    public async Task<IActionResult> Create([FromBody] GraveCreateDto graveDto)
    {

        var status = context.GraveStatuses
            .ToList()
            .Where(graveStatus => graveStatus.Name.Equals("Available"))
            .Select(graveStatus => graveStatus.Id)
            .ToList();
        
        var grave = new Grave
        {
            Location = graveDto.Location,
            Price = graveDto.Price,
            StatusId = status[0]
        };
        
        await context.Graves.AddAsync(grave);
        await context.SaveChangesAsync();

        var outputGraveDto = graveMapper.MapToDto(grave);

        return CreatedAtAction(nameof(Get), new { id = grave.Id }, outputGraveDto);
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
}