using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/singUpToLesson")]
public class SingUpToLesson : ControllerBase
{
    private readonly ISingUpToLessonService _singUpToLessonService;

    public SingUpToLesson(ISingUpToLessonService singUpToLessonService)
    {
        _singUpToLessonService = singUpToLessonService;
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> SingUp(SingUpToLessonDTO singUpToLessonDTO)
    {
        var singUpToLessonStatus = await _singUpToLessonService.SingUpToLessonAsync(singUpToLessonDTO);
        switch (singUpToLessonStatus)
        {
            case SingUpToLessonStatus.INVALID_EMAIL:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid User");
                break;
            case SingUpToLessonStatus.INVALID_SUBJECT:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Subject");
                break;
            case SingUpToLessonStatus.OK:
                return Ok();
                break;
            case SingUpToLessonStatus.DATABASE_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
                break;
            case SingUpToLessonStatus.INVALID_LESSON:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Lesson");
                break;
            case SingUpToLessonStatus.CONFLICT_LESSON:
                return StatusCode(StatusCodes.Status409Conflict, "Conflict with another lesson");
                break;
            case SingUpToLessonStatus.NOT_AVAILABILITY:
                return StatusCode(StatusCodes.Status422UnprocessableEntity, "Teacher is not available");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
        }
    }
}