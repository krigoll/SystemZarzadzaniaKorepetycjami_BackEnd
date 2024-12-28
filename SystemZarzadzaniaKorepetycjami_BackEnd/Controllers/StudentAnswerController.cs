using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/studentAnswer")]
public class StudentAnswerController : ControllerBase
{
    private readonly IStudentAnswerService _studentAnswerService;

    public StudentAnswerController(IStudentAnswerService studentAnswerService)
    {
        _studentAnswerService = studentAnswerService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAndUpdateStudentAnswer(int idTestForStudent,
        List<StudentAnswerDTO> studentAnswer)
    {
        var studentAnswersStatus =
            await _studentAnswerService.CreateAndUpdateStudentAnswer(idTestForStudent, studentAnswer);
        switch (studentAnswersStatus)
        {
            case StudentAnswersStatus.INVALID_IT_TEST_FOR_STUDENT:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Id Test For Student");
                break;
            case StudentAnswersStatus.OK:
                return Ok();
                break;
            case StudentAnswersStatus.INVALID_STUDENT_ANSWER:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Student Answer");
                break;
            case StudentAnswersStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
        }
    }
}