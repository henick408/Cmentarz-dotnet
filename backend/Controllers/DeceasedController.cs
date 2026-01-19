using Cmentarz.Data;
using Cmentarz.Dto.Deceased;
using Cmentarz.Mappers.Deceased;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cmentarz.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeceasedController(GraveyardDbContext context, IDeceasedMapper deceasedMapper) : ControllerBase
{

    [HttpGet]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> GetAll()
    {
        var deceaseds = await context.Deceaseds
            .ToListAsync();

        var deceadedDtos = deceaseds.Select(deceasedMapper.MapToReadDto);

        return Ok(deceadedDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var deceased = await context.Deceaseds
            .FindAsync(id);

        if (deceased == null)
        {
            return NotFound("Grave with given id does not exist");
        }

        var deceasedDto = deceasedMapper.MapToReadDto(deceased);

        return Ok(deceasedDto);

    }

    [HttpPost]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> Create([FromBody] DeceasedCreateDto deceasedDto)
    {
        var grave = await context.Graves
            .Include(g => g.Deceased)
            .FirstOrDefaultAsync(g => g.Id == deceasedDto.GraveId);

        if (grave == null)
        {
            return BadRequest("Grave does not exist");
        }

        if (grave.Deceased != null)
        {
            return BadRequest("Grave already taken");
        }

        var deceased = deceasedMapper.MapFromCreateDto(deceasedDto);

        var availableStatus = await context.GraveStatuses.FirstAsync(status => status.Name == "Available");
        
        grave.Deceased = deceased;
        grave.StatusId = availableStatus.Id;
        grave.Status = await context.GraveStatuses.FindAsync(1);
        
        context.Deceaseds.Add(deceased);
        await context.SaveChangesAsync();

        var outputDeceasedDto = deceasedMapper.MapToReadDto(deceased);

        return CreatedAtAction(nameof(Get), new { id = outputDeceasedDto.Id }, outputDeceasedDto);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> Delete(int id)
    {
        var deceased = await context.Deceaseds.FindAsync(id);
        if (deceased == null)
        {
            return NotFound("Deceased with given id does not exist");
        }

        context.Deceaseds.Remove(deceased);
        await context.SaveChangesAsync();

        return NoContent();
    }
    
}