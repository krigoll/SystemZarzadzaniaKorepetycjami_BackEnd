using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/resetPassword")]
public class ResetPasswordController : ControllerBase
{
    private readonly IResetPasswordService _resetPasswordService;

    public ResetPasswordController(IResetPasswordService resetPasswordService)
    {
        _resetPasswordService = resetPasswordService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCodeAsync(string email)
    {
        var codeStatus = await _resetPasswordService.CreateCodeAsync(email);
        switch (codeStatus)
        {
            case CodeStatus.OK:
                return Ok();
                break;
            case CodeStatus.INVALID_EMAIL:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Email");
                break;
            case CodeStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }

    [HttpPut("reset")]
    public async Task<IActionResult> ResetPasswordAsync(string code, string password)
    {
        var resetStatus = await _resetPasswordService.ResetPasswordAsync(code, password);
        switch (resetStatus)
        {
            case ResetStatus.OK:
                return Ok();
                break;
            case ResetStatus.INVALID_CODE:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Code");
                break;
            case ResetStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }

    [HttpPut("resetWitOutCode")]
    [Authorize]
    public async Task<IActionResult> ResetPasswordWitOutCodeAsync(string password)
    {
        var personIdClaim = User?.FindFirst("idPerson");

        if (personIdClaim == null) return Unauthorized("Nie znaleziono identyfikatora osoby");

        var personId = personIdClaim.Value;
        var resetStatus = await _resetPasswordService.ResetPasswordWitOutCodeAsync(password, int.Parse(personId));
        switch (resetStatus)
        {
            case ResetStatus.OK:
                return Ok();
                break;
            case ResetStatus.INVALID_CODE:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Code");
                break;
            case ResetStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }
}