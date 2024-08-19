using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly IPersonService _personService;

    public LoginController(ILoginService loginService, IPersonService personService)
    {
        _loginService = loginService;
        _personService = personService;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO request)
    {
        var loginStatus = await _loginService.LoginUserByEmailAndPasswordAsync(new LoginDTO
            { Email = request.Email, Password = request.Password });

        if (loginStatus == LoginStatus.USER_NOT_EXISTS)
            return StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized");

        if (loginStatus == LoginStatus.DATABASE_ERROR)
            return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");

        var personRole = await _personService.GetPersonRoleAsync(request.Email);

        var claims = new Claim[]
        {
            new("isAdmin", personRole.isAdmin.ToString()),
            new("isTeacher", personRole.isTeacher.ToString()),
            new("isStudent", personRole.isStudent.ToString())
        };

        var builder = WebApplication.CreateBuilder();
        var config = builder.Configuration;
        var secret = config["Jwt:Secret"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var options = new JwtSecurityToken("https://localhost:5001", "https://localhost:5001",
            claims, expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: creds);

        var refreshToken = "";
        using (var genNum = RandomNumberGenerator.Create())
        {
            var r = new byte[32];
            genNum.GetBytes(r);
            refreshToken = Convert.ToBase64String(r);
        }

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(options),
            refreshToken
        });
    }

    [HttpPost]
    public IActionResult CreateUser(LoginDTO request)
    {
        var hasher = new PasswordHasher<LoginDTO>();
        var hashedPassword = hasher.HashPassword(request, request.Password);

        return Ok(hashedPassword);
    }


    [HttpPost("refresh")]
    public IActionResult RefreshToken([FromBody] string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest("Invalid refresh token");
        }

        var validToken = ValidateRefreshToken(refreshToken);
        if (validToken == null)
        {
            return Unauthorized("Invalid refresh token");
        }

        var claims = new Claim[]
        {
            new Claim("isAdmin", validToken.Claims.FirstOrDefault(c => c.Type == "isAdmin")?.Value ?? "false"),
            new Claim("isTeacher", validToken.Claims.FirstOrDefault(c => c.Type == "isTeacher")?.Value ?? "false"),
            new Claim("isStudent", validToken.Claims.FirstOrDefault(c => c.Type == "isStudent")?.Value ?? "false")
        };

        var builder = WebApplication.CreateBuilder();
        var config = builder.Configuration;
        var secret = config["Jwt:Secret"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var newAccessToken = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: creds
        ));

        var newRefreshToken = GenerateRefreshToken();

        return Ok(new
        {
            token = newAccessToken,
            refreshToken = newRefreshToken
        });
    }


    private ClaimsPrincipal ValidateRefreshToken(string refreshToken)
    {
        var claims = new[]
        {
            new Claim("isAdmin", "true"),
            new Claim("isTeacher", "true"),
            new Claim("isStudent", "true")
        };

        var identity = new ClaimsIdentity(claims, "custom");

        return new ClaimsPrincipal(identity);
    }

    private string GenerateRefreshToken()
    {
        using (var genNum = RandomNumberGenerator.Create())
        {
            var r = new byte[32];
            genNum.GetBytes(r);
            return Convert.ToBase64String(r);
        }
    }
}