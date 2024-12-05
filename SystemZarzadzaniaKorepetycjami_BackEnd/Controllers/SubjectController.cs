using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/subject")]
public class SubjectController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpGet("getAllSubjects")]
    [Authorize]
    public async Task<IActionResult> GetAllSubjects()
    {
        try
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            return Ok(subjects);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("getAllSubjectsByEmail")]
    [Authorize]
    public async Task<IActionResult> GetAllSubjectsEdit(string email)
    {
        try
        {
            var subjects = await _subjectService.GetAllSubjectsEditAsync(email);
            return subjects == null
                ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Teacher Email")
                : Ok(subjects);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateSubjectAsync(string subjectName)
    {
        var isAdminClaim = HttpContext.User.FindFirst("isAdmin")?.Value;

        if (isAdminClaim == null || !bool.TryParse(isAdminClaim, out var isAdmin) || !isAdmin) return Forbid();

        var subjectStatus = await _subjectService.CreateSubjectAsync(subjectName);
        switch (subjectStatus)
        {
            case SubjectStatus.OK:
                return Ok();
                break;
            case SubjectStatus.INVALID_SUBJECT:
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Subject");
                break;
            case SubjectStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }

    [HttpPut("{idSubject}")]
    [Authorize]
    public async Task<IActionResult> UpdateSubjectAsync(int idSubject, string subjectName)
    {
        var isAdminClaim = HttpContext.User.FindFirst("isAdmin")?.Value;

        if (isAdminClaim == null || !bool.TryParse(isAdminClaim, out var isAdmin) || !isAdmin) return Forbid();

        var subjectStatus = await _subjectService.UpdateSubjectAsync(idSubject, subjectName);
        switch (subjectStatus)
        {
            case SubjectStatus.OK:
                return Ok();
                break;
            case SubjectStatus.INVALID_SUBJECT:
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Subject");
                break;
            case SubjectStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            case SubjectStatus.INVALID_SUBJECT_ID:
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Subject Id");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }
}