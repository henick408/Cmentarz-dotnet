namespace Cmentarz.Models;

public class Deceased
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateOnly BirthDate { get; set; }
    public required DateOnly DeathDate { get; set; }
    

}