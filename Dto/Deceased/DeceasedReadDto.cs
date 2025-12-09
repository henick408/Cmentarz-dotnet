namespace Cmentarz.Dto.Deceased;

public class DeceasedReadDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly DeathDate { get; set; }
    public int GraveId { get; set; }
}