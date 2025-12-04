using Cmentarz.Data;
using Cmentarz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cmentarz.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class GraveStatusController(GraveyardDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var statuses = await context.GraveStatuses.ToListAsync();
        return Ok(statuses);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var status = await context.GraveStatuses.FindAsync(id);

        if (status == null)
        {
            return NotFound();
        }

        return Ok(status);
    }

    [HttpPost]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> Create([FromBody] GraveStatus status)
    {
        if (string.IsNullOrWhiteSpace(status.Name))
        {
            return BadRequest("Name cannot be empty");
        }

        context.GraveStatuses.Add(status);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = status.Id }, status);
    }
    
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> Update(int id, [FromBody] GraveStatus updated)
    {
        var status = await context.GraveStatuses.FindAsync(id);

        if (status == null)
            return NotFound();

        status.Name = updated.Name;

        await context.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Employee")] 
    public async Task<IActionResult> Delete(int id)
    {
        var status = await context.GraveStatuses.FindAsync(id);

        if (status == null)
            return NotFound();

        context.GraveStatuses.Remove(status);
        await context.SaveChangesAsync();

        return NoContent();
    }
}