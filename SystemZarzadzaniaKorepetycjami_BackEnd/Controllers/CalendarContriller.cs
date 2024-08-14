using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/calendar")]
public class CalendarContriller : ControllerBase
{
    private readonly ICalendarService _calendarService;

    public CalendarContriller(ICalendarService calendarService)
    {
        _calendarService = calendarService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCalendarByEmailAndDate(string date, string email)
    {
        try
        {
            var calendars =
                await _calendarService.GetCalendarsFromTheWeekByDateAndTeacherEmail(DateOnly.Parse(date), email);
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