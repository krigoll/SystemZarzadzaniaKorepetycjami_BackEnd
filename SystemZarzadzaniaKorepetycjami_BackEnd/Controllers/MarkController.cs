using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/mark")]
public class MarkController : ControllerBase
{
    private readonly IMarkService _markService;

    public MarkController(IMarkService markService)
    {
        _markService = markService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAndUpdateMark(List<MarkDTO> marks)
    {
        var markStatus = await _markService.CreateAndUpdateMark(marks);
        switch (markStatus)
        {
            case MarkStatus.INVALID_MARK:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Mark");
                break;
            case MarkStatus.OK:
                return Ok();
                break;
            case MarkStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
        }
    }
}