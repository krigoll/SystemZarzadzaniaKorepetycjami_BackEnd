using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/calendar")]
public class CalendarController : ControllerBase
{
    private readonly ICalendarService _calendarService;

    public CalendarController(ICalendarService calendarService)
    {
        _calendarService = calendarService;
    }

    [HttpGet]
    //[Authorize]
    public async Task<IActionResult> GetCalendarsByEmailAndDate(string email, string date)
    {
        try
        {
            var calendars =
                await _calendarService.GetCalendarsFromTheWeekByDateAndTeacherEmail(DateOnly.Parse(date),email);
            return calendars == null
                ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Email")
                : Ok(calendars);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }
}