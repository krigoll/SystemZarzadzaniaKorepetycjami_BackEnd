﻿using Microsoft.AspNetCore.Mvc;
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

    [HttpPost("registration")] //hasła nie dodales ja
    public async Task<IActionResult> Registration(RegistrationDTO registrationDto)
    {
        var registrationStatus = await _personService.RegistrationPerson(registrationDto);
        switch (registrationStatus)
        {
            case RegisterStarus.INVALID_USER:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid User");
                break;
            case RegisterStarus.REGISTERED_USER:
                return Ok();
                break;
            case RegisterStarus.DATEBASE_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
        }
    }
}