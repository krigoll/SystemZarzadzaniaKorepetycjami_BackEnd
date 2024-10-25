using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/person")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Registration(RegistrationDTO registrationDto)
    {
        var registrationStatus = await _personService.RegistrationPerson(registrationDto);
        switch (registrationStatus)
        {
            case RegisterStatus.INVALID_USER:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid User");
                break;
            case RegisterStatus.REGISTERED_USER:
                return Ok();
                break;
            case RegisterStatus.DATEBASE_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
                break;
            case RegisterStatus.EMAIL_NOT_UNIQUE:
                return StatusCode(StatusCodes.Status409Conflict, "Not unique email");
                break;
            case RegisterStatus.PHONE_NUMBER_NOT_UNIQUE:
                return StatusCode(StatusCodes.Status409Conflict, "Not unique phone number");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
        }
    }

    [HttpPut("{id}/update")]
    [Authorize]
    public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] PersonEditProfileDTO personProfileDto)
    {
        var registrationStatus = await _personService.UpdateUserAsync(id, personProfileDto);
        switch (registrationStatus)
        {
            case UpdateUserStatus.INVALID_USER:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid User");
                break;
            case UpdateUserStatus.UPDATED_USER:
                return Ok();
                break;
            case UpdateUserStatus.DATEBASE_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
                break;
            case UpdateUserStatus.EMAIL_NOT_UNIQUE:
                return StatusCode(StatusCodes.Status409Conflict, "Not unique email");
                break;
            case UpdateUserStatus.PHONE_NUMBER_NOT_UNIQUE:
                return StatusCode(StatusCodes.Status409Conflict, "Not unique phone number");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
        }
    }

    [HttpGet("getUser")]
    [Authorize]
    public async Task<IActionResult> GetUserInformationAsync(string email)
    {
        try
        {
            var personProfileDto = await _personService.GetPersonProfileByEmailAsync(email);
            return personProfileDto == null
                ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Email")
                : Ok(personProfileDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
        }
    }

    [HttpDelete("delete")]
    [Authorize]
    public async Task<IActionResult> DeleteUserByEmailAsync(string email)
    {
        try
        {
            await _personService.DeleteUserByEmailAsync(email);
            return Ok();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
        }
    }

    [HttpGet("{search}")]
    //[Authorize]
    public async Task<IActionResult> FindPersonsByNameOrSurname(string search)
    {
        var people = await _personService.FindPersonsByNameOrSurname(search);
        return Ok(people);
    }
}