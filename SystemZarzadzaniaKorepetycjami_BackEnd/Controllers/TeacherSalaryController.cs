using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/teacherSalary")]
public class TeacherSalaryController : ControllerBase
{
    private readonly ITeacherSalaryService _teacherSalaryServiceService;

    public TeacherSalaryController(ITeacherSalaryService teacherSalaryServiceService)
    {
        _teacherSalaryServiceService = teacherSalaryServiceService;
    }

    [HttpPost("setTeacherSalary")]
    public async Task<IActionResult> SetTeacherSalarysAsync(List<TeacherSalaryDTO> teacherSalaryDTO)
    {
        var teacherSalaryStatus = await _teacherSalaryServiceService.setTeacherSalaryAsync(teacherSalaryDTO);
        switch (teacherSalaryStatus)
        {
            case TeacherSalaryStatus.INVALID_TEACHER_SALARY:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Teacher Salary");
                break;
            case TeacherSalaryStatus.VALID_TEACHER_SALARY:
                return Ok();
                break;
            case TeacherSalaryStatus.DATEBASE_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
        }
    }
}