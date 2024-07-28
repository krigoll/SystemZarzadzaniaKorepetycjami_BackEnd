using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
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
            var subjects = await _subjectService.getAllSubjects();
            return Ok(subjects);
        }
        catch (Exception ex) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}