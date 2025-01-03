using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/teacher")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetTeachersBySubjectCategoryAsync(int subjectLevelId, string email)
    {
        try
        {
            var teachers = await _teacherService.GetTeachersBySubjectCategoryAsync(subjectLevelId, email);
            return Ok(teachers);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("studentEmail")]
    [Authorize]
    public async Task<IActionResult> GetAllTeachersThatTeachStudentByStudentEmail(string studentEmail)
    {
        var teachers = await _teacherService.GetAllTeachersThatTeachStudentByStudentEmail(studentEmail);
        return Ok(teachers);
    }
}