using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/availability")]
public class AvailabilityController : ControllerBase
{
    private readonly IAvailabilityService _availabilityService;

    public AvailabilityController(IAvailabilityService availabilityService)
    {
        _availabilityService = availabilityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAvailabilityByEmail(string email)
    {
        try
        {
            var availabilitys =
                await _availabilityService.GetTeacherAvailabilityByEmailAsync(email);
            return availabilitys == null
                ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Email")
                : Ok(availabilitys);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAndUpdateAvailabilitiesByEmail(string email,
        List<AvailabilityDTO> availabilities)
    {
        var setAvailabilityStatus =
            await _availabilityService.CreateAndUpdateAvailabilityByEmail(email, availabilities);
        switch (setAvailabilityStatus)
        {
            case SetAvailabilityStatus.INVALID_EMAIL:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid User");
                break;
            case SetAvailabilityStatus.INVALID_TIME:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Time");
                break;
            case SetAvailabilityStatus.OK:
                return Ok();
                break;
            case SetAvailabilityStatus.DATABASE_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
        }
    }
}