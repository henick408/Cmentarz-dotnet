using System.ComponentModel.DataAnnotations;

namespace Cmentarz.Models;

public class User
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string Role { get; set; }

    public ICollection<Grave> Graves { get; set; } = new List<Grave>();
}