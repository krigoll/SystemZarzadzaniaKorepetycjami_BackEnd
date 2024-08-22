using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
    public async Task<IActionResult> SingUp(SingUpToLessonDTO singUpToLessonDTO)
    {
        var singUpToLessonStatus = await _singUpToLessonService.SingUpToLessonAndSendMessageAsync(singUpToLessonDTO);
        switch (singUpToLessonStatus)
        {
            case SingUpToLessonStaus.INVALID_EMAIL:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid User");
                break;
            case SingUpToLessonStaus.INVALID_SUBJECT:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Subject");
                break;
            case SingUpToLessonStaus.OK:
                return Ok();
                break;
            case SingUpToLessonStaus.DATABASE_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
                break;
            case SingUpToLessonStaus.INVALID_LESSON:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Lesson"); 
                break;    
            case SingUpToLessonStaus.CONFLICT_LESSON:
                return StatusCode(StatusCodes.Status409Conflict, "Conflict with another lesson"); 
                break;  
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
        }
        
    }
}