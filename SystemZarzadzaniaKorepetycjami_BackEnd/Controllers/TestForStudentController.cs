using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/testForStudent")]
public class TestForStudentController : ControllerBase
{
    private readonly ITestForStudentService _testForStudentService;

    public TestForStudentController(ITestForStudentService testForStudentService)
    {
        _testForStudentService = testForStudentService;
    }

    [HttpGet("{idTestForStudent}")]
    [Authorize]
    public async Task<IActionResult> GetTestForStudentDetails(int idTestForStudent)
    {
        var test = await _testForStudentService.GetTestForStudentDetails(idTestForStudent);

        return test == null
            ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Test For Student Id")
            : Ok(test);
    }

    [HttpGet("teacher/{idTeacher}")]
    [Authorize]
    public async Task<IActionResult> GetGivenTestsByTeacherAsync(int idTeacher)
    {
        var tests = await _testForStudentService.GetGivenTestsByTeacherAsync(idTeacher);

        return tests == null
            ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Teacher Id")
            : Ok(tests);
    }

    [HttpGet("student/{idStudent}")]
    [Authorize]
    public async Task<IActionResult> GetGivenTestsByStudentAsync(int idStudent)
    {
        var tests = await _testForStudentService.GetGivenTestsByStudentAsync(idStudent);

        return tests == null
            ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Student Id")
            : Ok(tests);
    }

    [HttpPut]
    public async Task<IActionResult> GetGiveTestToStudentAsync(int idStudent, int idTest)
    {
        var testForStudentStatus = await _testForStudentService.GetGiveTestToStudentAsync(idStudent, idTest);
        switch (testForStudentStatus)
        {
            case TestForStudentStatus.INVALID_TEST_ID:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Test Id");
                break;
            case TestForStudentStatus.OK:
                return Ok();
                break;
            case TestForStudentStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            case TestForStudentStatus.INVALID_STUDENT_ID:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Student Id");
                break;
            case TestForStudentStatus.INVALID_TEST_FOR_STUDENT:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Test For Student");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }
}