using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly ITestService _testService;

    public TestController(ITestService testService)
    {
        _testService = testService;
    }

    [HttpGet("{idTest}")]
    [Authorize]
    public async Task<IActionResult> GetTestWithAssignmentsByTestIdAsync(int idTest)
    {
        var testWithAssignments = await _testService.GetTestWithAssignmentsByTestIdAsync(idTest);

        return testWithAssignments == null
            ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Test Id")
            : Ok(testWithAssignments);
    }

    [HttpGet("teacher/{idTeacher}")]
    [Authorize]
    public async Task<IActionResult> GetTestsByTeacherIdAsync(int idTeacher)
    {
        var testsByTeacherId = await _testService.GetTestsByTeacherIdAsync(idTeacher);

        return testsByTeacherId == null
            ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Teacher Id")
            : Ok(testsByTeacherId);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> CreateTestAsync(int idTeacher, string title)
    {
        var createTestStatus = await _testService.CreateTestAsync(idTeacher, title);
        switch (createTestStatus)
        {
            case TestStatus.INVALID_TEST:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Test");
                break;
            case TestStatus.OK:
                return Ok();
                break;
            case TestStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            case TestStatus.INVALID_TEACHER_ID:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Teacher Id");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }

    [HttpPost("{idTest}/update")]
    [Authorize]
    public async Task<IActionResult> UpdateTestByIdAsync(int idTest, string title)
    {
        var createTestStatus = await _testService.UpdateTestByIdAsync(idTest, title);
        switch (createTestStatus)
        {
            case TestStatus.INVALID_TEST:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Test");
                break;
            case TestStatus.OK:
                return Ok();
                break;
            case TestStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            case TestStatus.INVALID_TEACHER_ID:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Teacher Id");
                break;
            case TestStatus.INVALID_TEST_ID:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Test Id");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }

    [HttpPut("{idTest}/addAssignment")]
    [Authorize]
    public async Task<IActionResult> AddAssigmentToTestAsync(int idTest, AssignmentDTO assignmentDto)
    {
        var modifyAssigmentToTestStatus = await _testService.AddAssigmentToTestAsync(idTest, assignmentDto);
        switch (modifyAssigmentToTestStatus)
        {
            case AssignmentToTestStatus.INVALID_TEST_ID:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Test Id");
                break;
            case AssignmentToTestStatus.OK:
                return Ok();
                break;
            case AssignmentToTestStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            case AssignmentToTestStatus.INVALID_ASSIGMENT_ID:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Assigment Id");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }

    [HttpDelete("{idTest}/{idAssigment}")]
    [Authorize]
    public async Task<IActionResult> RemoveAssigmentFromTestAsync(int idTest, int idAssigment)
    {
        var modifyAssigmentToTestStatus = await _testService.RemoveAssigmentFromTestAsync(idTest, idAssigment);
        switch (modifyAssigmentToTestStatus)
        {
            case AssignmentToTestStatus.INVALID_TEST_ID:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Test Id");
                break;
            case AssignmentToTestStatus.OK:
                return Ok();
                break;
            case AssignmentToTestStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            case AssignmentToTestStatus.INVALID_ASSIGMENT_ID:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Assigment Id");
                break;
            case AssignmentToTestStatus.ASSIGMENT_NOT_ON_THAT_TEST:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Assigment On Test");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }

    [HttpDelete("{idTest}")]
    [Authorize]
    public async Task<IActionResult> DeleteTestByIdAsync(int idTest)
    {
        var isDeleted = await _testService.DeleteTestByIdAsync(idTest);
        return isDeleted ? Ok() : StatusCode(StatusCodes.Status400BadRequest, "Invalid Test Id");
    }
}