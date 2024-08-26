using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO request)
    {
        var (status, jwtToken, refreshToken) = await _loginService.LoginAsync(request);

        if (status == LoginStatus.USER_NOT_EXISTS)
            return StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized");

        if (status == LoginStatus.DATABASE_ERROR)
            return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");

        return Ok(new { token = jwtToken, refreshToken });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        var newAccessToken = await _loginService.RefreshTokenAsync(refreshToken);

        if (newAccessToken == null)
            return Unauthorized("Invalid refresh token");

        return Ok(new { token = newAccessToken });
    }

    [HttpPost]
    public IActionResult CreateUser(LoginDTO request)
    {
        var hashedPassword = _loginService.HashPassword(request);
        return Ok(hashedPassword);
    }
}