using Cmentarz.Data;
using Cmentarz.Mappers.Deceased;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cmentarz.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeceasedController(GraveyardDbContext context, IDeceasedMapper deceasedMapper) : ControllerBase
{

    [HttpGet]
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
            return NotFound();
        }

        var deceasedDto = deceasedMapper.MapToReadDto(deceased);

        return Ok(deceasedDto);

    }
    
}