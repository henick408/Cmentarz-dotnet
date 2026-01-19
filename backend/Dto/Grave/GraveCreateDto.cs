using System.ComponentModel.DataAnnotations;

namespace Cmentarz.Dto.Grave;

public class GraveCreateDto
{
    [Required]
    public string Location { get; set; }
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
}