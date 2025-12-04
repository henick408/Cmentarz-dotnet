namespace Cmentarz.Models;

public class GraveStatus
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public ICollection<Grave> Graves { get; set; } = new List<Grave>();
}