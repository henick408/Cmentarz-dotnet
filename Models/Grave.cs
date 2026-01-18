namespace Cmentarz.Models;

public class Grave
{
    public int Id { get; set; }
    
    public required string Location { get; set; }
    public required decimal Price { get; set; }
    
    public required int StatusId { get; set; }
    public GraveStatus? Status { get; set; }
    
    public int? OwnerId { get; set; }
    public User? Owner { get; set; }
    
    public Deceased? Deceased { get; set; }

}