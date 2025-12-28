using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cmentarz.Data;
using Cmentarz.Dto.Auth;
using Cmentarz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Cmentarz.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController(GraveyardDbContext context, IConfiguration config) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = context.Users.SingleOrDefault(user => user.Email == request.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Unauthorized();
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(config["JwtKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            ]),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Ok(new { token = tokenHandler.WriteToken(token) });
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
    {
        var emailExists = await context.Users.AnyAsync((user => user.Email == requestDto.Email));

        if (emailExists)
        {
            return BadRequest("User already exists");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(requestDto.Password);

        var user = new User
        {
            Email = requestDto.Email,
            PasswordHash = passwordHash,
            Role = "User"
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return Ok("User registered successfully");
    }
}