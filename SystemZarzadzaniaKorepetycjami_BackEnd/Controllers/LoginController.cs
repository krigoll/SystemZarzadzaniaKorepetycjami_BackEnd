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

    [HttpPost("api/refresh")]
    public IActionResult RefreshToken()
    {
        return Ok();
    }
}