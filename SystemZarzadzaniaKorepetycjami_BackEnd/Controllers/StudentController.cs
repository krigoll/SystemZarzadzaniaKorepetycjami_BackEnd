using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/student")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet("{idTeacher}")]
    [Authorize]
    public async Task<IActionResult> GetStudentsThatTeachOrWillByTeachTeacherByTeacherIdAsync(int idTeacher)
    {
        var students = await _studentService.GetStudentsThatTeachOrWillByTeachTeacherByTeacherIdAsync(idTeacher);

        return students == null
            ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Teacher Id")
            : Ok(students);
    }
}