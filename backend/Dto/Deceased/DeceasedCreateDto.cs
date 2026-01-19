using System.ComponentModel.DataAnnotations;

namespace Cmentarz.Dto.Deceased;

public class DeceasedCreateDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public DateOnly BirthDate { get; set; }
    [Required]
    public DateOnly DeathDate { get; set; }
    [Required]
    public int GraveId { get; set; }
}