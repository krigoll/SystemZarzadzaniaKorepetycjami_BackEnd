using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        //   Check login and password
        //   Fetch data from database
        // var hasher = new PasswordHasher<LoginRequest>();
        // if (hasher.VerifyHashedPassword(request, "sss", request.Password)==PasswordVerificationResult.Failed)
        // {
        //     //... 401
        // }

        var claims = new Claim[]
        {
            new(ClaimTypes.Name, "jan123"),
            new("Custom", "SomeData"),
            new Claim(ClaimTypes.Role, "admin")
        };

        var secret = "sdn209jsldjp29jdlsjdlsjdisjdklmlmjklml2me9wj92292992929esklmdsmdsmd.sdsd";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    
        var options = new JwtSecurityToken("https://localhost:5001", "https://localhost:5001",
            claims, expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: creds);

        var refreshToken = "";
        using (var genNum = RandomNumberGenerator.Create())
        {
            var r = new byte[1024];
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
        var hashedPassword=hasher.HashPassword(request, request.Password);
    
        return Ok(hashedPassword);
    }

    [HttpPost("api/refresh")]
    public IActionResult RefreshToken()
    {
        return Ok();
    }
}