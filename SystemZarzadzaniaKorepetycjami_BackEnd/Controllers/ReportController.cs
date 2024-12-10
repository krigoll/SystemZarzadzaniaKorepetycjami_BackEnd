using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/report")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllReportsAsync()
    {
        var isAdminClaim = HttpContext.User.FindFirst("isAdmin")?.Value;

        if (isAdminClaim == null || !bool.TryParse(isAdminClaim, out var isAdmin) || !isAdmin) return Forbid();

        var reports = await _reportService.GetAllReportsAsync();
        return Ok(reports);
    }

    [HttpGet("{idReport}")]
    [Authorize]
    public async Task<IActionResult> GetReportDetailsByIdAsync(int idReport)
    {
        var isAdminClaim = HttpContext.User.FindFirst("isAdmin")?.Value;

        if (isAdminClaim == null || !bool.TryParse(isAdminClaim, out var isAdmin) || !isAdmin) return Forbid();

        var report = await _reportService.GetReportDetailsByIdAsync(idReport);
        return report == null ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Report Id") : Ok(report);
    }

    [HttpGet("notDealted")]
    [Authorize]
    public async Task<IActionResult> GetAllNotDeltedReportsAsync()
    {
        var isAdminClaim = HttpContext.User.FindFirst("isAdmin")?.Value;

        if (isAdminClaim == null || !bool.TryParse(isAdminClaim, out var isAdmin) || !isAdmin) return Forbid();

        var reports = await _reportService.GetAllNotDeltedReportsAsync();
        return Ok(reports);
    }

    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateRepostAsync(ReportDetailsDTO report)
    {
        var reportStatus = await _reportService.CreateRepostAsync(report);
        switch (reportStatus)
        {
            case ReportStatus.OK:
                return Ok();
                break;
            case ReportStatus.INVALID_REPORT:
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Report");
                break;
            case ReportStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            case ReportStatus.INVALID_SENDER_ID:
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Sender Id");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }

    [HttpPut("{idReport}/update")]
    [Authorize]
    public async Task<IActionResult> UpdateRepostAsync(int idReport, ReportDetailsDTO report)
    {
        var isAdminClaim = HttpContext.User.FindFirst("isAdmin")?.Value;

        if (isAdminClaim == null || !bool.TryParse(isAdminClaim, out var isAdmin) || !isAdmin) return Forbid();

        var reportStatus = await _reportService.UpdateRepostAsync(idReport, report);
        switch (reportStatus)
        {
            case ReportStatus.OK:
                return Ok();
                break;
            case ReportStatus.INVALID_REPORT:
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Report");
                break;
            case ReportStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            case ReportStatus.INVALID_SENDER_ID:
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Sender Id");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }
}