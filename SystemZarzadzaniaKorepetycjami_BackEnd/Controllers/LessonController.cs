using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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

    [HttpPut("{lessonId}/accept")]
    [Authorize]
    public async Task<IActionResult> AcceptLessonByLessonIdAsync(int lessonId)
    {
        var accepted = await _lessonService.AcceptLessonByIdAsync(lessonId);
        if (accepted)
            return Ok();
        return StatusCode(StatusCodes.Status400BadRequest, "Invalid Lesson Id");
    }

    [HttpPut("{lessonId}/reject")]
    [Authorize]
    public async Task<IActionResult> RejectLessonByLessonIdAsync(int lessonId)
    {
        var accepted = await _lessonService.RejectLessonByIdAsync(lessonId);
        if (accepted)
            return Ok();
        return StatusCode(StatusCodes.Status400BadRequest, "Invalid Lesson Id");
    }

    [HttpPut("{lessonId}/cancel")]
    [Authorize]
    public async Task<IActionResult> CancelLessonByLessonIdAsync(int lessonId)
    {
        var accepted = await _lessonService.CancelLessonByIdAsync(lessonId);
        if (accepted)
            return Ok();
        return StatusCode(StatusCodes.Status400BadRequest, "Invalid Lesson Id");
    }

    [HttpGet("getLessonDetails")]
    [Authorize]
    public async Task<IActionResult> GetLessonDetailsByIdAsync(int lessonId)
    {
        var LessonDatailsDTO = await _lessonService.GetLessonDetailsByIdAsync(lessonId);

        return LessonDatailsDTO == null
            ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Lesson Id")
            : Ok(LessonDatailsDTO);
    }
}