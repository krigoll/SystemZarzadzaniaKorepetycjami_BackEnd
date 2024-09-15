using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

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
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
        }
        
    }
}