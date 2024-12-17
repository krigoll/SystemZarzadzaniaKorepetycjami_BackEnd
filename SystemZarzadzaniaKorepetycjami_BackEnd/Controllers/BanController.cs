using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

[ApiController]
[Route("api/ban")]
public class BanController : ControllerBase
{
    private readonly IBanService _banService;

    public BanController(IBanService banService)
    {
        _banService = banService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllBansAsync()
    {
        var bans = await _banService.GetAllBansAsync();
        return Ok(bans);
    }

    [HttpGet("{idBan}")]
    [Authorize]
    public async Task<IActionResult> GetBanDetailsByIdAsync(int idBan)
    {
        var ban = await _banService.GetBanDetailsByIdAsync(idBan);
        return ban == null ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Ban Id") : Ok(ban);
    }

    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateBanAsync(BanDetailsDTO ban)
    {
        var banStatus = await _banService.CreateBanAsync(ban);
        switch (banStatus)
        {
            case BanStatus.OK:
                return Ok();
                break;
            case BanStatus.INVALID_BAN:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Ban");
                break;
            case BanStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            case BanStatus.INVALID_BANED_ID:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Bened Id");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }

    [HttpPut("{idBan}/update")]
    [Authorize]
    public async Task<IActionResult> UpdateBanAsync(int idBan, BanDetailsDTO ban)
    {
        var banStatus = await _banService.UpdateBanAsync(idBan, ban);
        switch (banStatus)
        {
            case BanStatus.OK:
                return Ok();
                break;
            case BanStatus.INVALID_BAN:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Ban");
                break;
            case BanStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            case BanStatus.INVALID_BANED_ID:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Baned Id");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }

    [HttpDelete("{idBan}/delete")]
    [Authorize]
    public async Task<IActionResult> DeleteBanByIdAsync(int idBan)
    {
        var isDeleted = await _banService.DeleteBanByIdAsync(idBan);
        return isDeleted ? Ok() : StatusCode(StatusCodes.Status400BadRequest, "Invalid Ban Id");
    }
}