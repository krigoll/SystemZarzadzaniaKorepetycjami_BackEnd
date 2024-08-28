using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/lesson")]
public class LessonController : ControllerBase
{
    private readonly ILessonService _lessonService;

    public LessonController(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [HttpGet]
    public async Task<IActionResult> GetReservedLessonsByTeacherEmail(string email)
    {
        try
        {
            var lessons =
                await _lessonService.getAllReservedLessonsByTeacherEmailAsync(email);
            return lessons == null
                ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Email")
                : Ok(lessons);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }
}