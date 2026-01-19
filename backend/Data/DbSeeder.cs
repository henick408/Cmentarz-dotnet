using Cmentarz.Models;

namespace Cmentarz.Data;

public static class DbSeeder
{
    public static void Seed(GraveyardDbContext context)
    {
        if (!context.Users.Any())
        {
            var employee = new User
            {
                Email = "employee@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = "Employee"
            };

            var user = new User
            {
                Email = "user@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!"),
                Role = "User"
            };

            context.Users.AddRange(employee, user);
            context.SaveChanges();
        }

        if (context.GraveStatuses.Any())
        {
            return;
        }
        var graveStatuses = new List<GraveStatus>
        {
            new() { Name = "Available" },
            new() {Name = "Reserved"},
            new() { Name = "Occupied" }
        };
        context.GraveStatuses.AddRange(graveStatuses); 
        context.SaveChanges();

    }
}