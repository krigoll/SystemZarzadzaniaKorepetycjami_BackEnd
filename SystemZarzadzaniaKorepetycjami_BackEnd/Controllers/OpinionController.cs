using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

//ogulne wuwagi do opinioon
//dołuż do delete student to że usuwanie wszystkich opinni, przemysleć to robić z teacher 
//sprawdzanie czy jest person usunięty czy zmienił role i inaczej to robić, inne opinie wysyłać
//sprawdzić czy można usunąc rekorz ale zostawić rzeczy połaczone z nim, odpowieć mozna, trzeba dodać sprawdzanie czy tym jest usuniety czy nie 

[ApiController]
[Route("api/opinion")]
public class OpinionController : ControllerBase
{
    private readonly IOpinionService _opinionService;

    public OpinionController(IOpinionService opinionService)
    {
        _opinionService = opinionService;
    }

    [HttpGet("getOpinionsByStudentEmail")]
    [Authorize]
    public async Task<IActionResult> GetOpinionsByStudentEmailAsync(string email)
    {
        var opinionDtoList = await _opinionService.GetOpinionsByStudentEmailAsync(email);

        return opinionDtoList == null
            ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Student Id")
            : Ok(opinionDtoList);
    }

    [HttpGet("getOpinionsByTeacherId")]
    [Authorize]
    public async Task<IActionResult> GetOpinionsByTeacherIdAsync(int teacherId)
    {
        var opinionDtoList = await _opinionService.GetOpinionsByTeacherIdAsync(teacherId);

        return opinionDtoList == null
            ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Teacher Id")
            : Ok(opinionDtoList);
    }

    [HttpGet("getOpinionsByTeacherEmail")]
    [Authorize]
    public async Task<IActionResult> GetOpinionsByTeacherEmailAsync(string email)
    {
        var opinionDtoList = await _opinionService.GetOpinionsByTeacherEmailAsync(email);

        return opinionDtoList == null
            ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Teacher Id")
            : Ok(opinionDtoList);
    }

    [HttpGet("{opinionId}")]
    [Authorize]
    public async Task<IActionResult> GetOpinionByIdAsync(int opinionId)
    {
        var opinionDto = await _opinionService.GetOpinionByIdAsync(opinionId);

        return opinionDto == null ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Opinion Id") : Ok(opinionDto);
    }

    [HttpPut("{opinionId}/update")]
    [Authorize]
    public async Task<IActionResult> UpdateOpinionByIdAsync(int opinionId, [FromBody] OpinionCreateDTO opinionCreateDTO)
    {
        var createOpinionStatus = await _opinionService.UpdateOpinionByIdAsync(opinionId, opinionCreateDTO);
        switch (createOpinionStatus)
        {
            case CreateOpinionStatus.INVALID_OPINION:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Opinion");
                break;
            case CreateOpinionStatus.OK:
                return Ok();
                break;
            case CreateOpinionStatus.INVALID_STUDENT_OR_TEACHER:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid student or teacher");
                break;
            default:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Opinion Id");
        }
    }

    [HttpDelete("{opinionId}/delete")]
    [Authorize]
    public async Task<IActionResult> DeleteOpinionByIdAsync(int opinionId)
    {
        if (await _opinionService.DeleteOpinionByIdAsync(opinionId))
            return Ok();
        return StatusCode(StatusCodes.Status400BadRequest, "Invalid Opinion Id");
    }

    [HttpDelete("deleteAll")]
    [Authorize]
    public async Task<IActionResult> DeleteAllOpinionsByEmailAsync(string email)
    {
        //usuwane wszystkich opinji o podanym email 
        return Ok();
    }

    [HttpPost("createOpinion")]
    [Authorize]
    public async Task<IActionResult> CreateOpinionAsync(OpinionCreateDTO opinionCreateDTO)
    {
        var createOpinionStatus = await _opinionService.CreateOpinionAsync(opinionCreateDTO);
        switch (createOpinionStatus)
        {
            case CreateOpinionStatus.INVALID_OPINION:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Opinion");
                break;
            case CreateOpinionStatus.OK:
                return Ok();
                break;
            case CreateOpinionStatus.INVALID_STUDENT_OR_TEACHER:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid student or teacher");
                break;
            case CreateOpinionStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            case CreateOpinionStatus.CAN_NOT_OPINION_TEACHER_WITCH_NOT_TEACHED_YOU:
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Cant no make opinion about teacher witch never teach you");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }
}