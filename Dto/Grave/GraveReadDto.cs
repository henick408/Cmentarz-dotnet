namespace Cmentarz.Dto.Grave;

public class GraveReadDto
{
    public int Id { get; set; }
    public string Location { get; set; }
    public decimal Price { get; set; }
    public int StatusId { get; set; }
    public int? OwnerId { get; set; }
}