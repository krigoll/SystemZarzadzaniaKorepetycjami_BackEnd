using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
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

    [HttpPost]
    public async Task<IActionResult> CreateAndUpdateCalerndarsByEmail(string email, List<CalendarDTO> calendars)
    {
        try
        {
            var resoult = await _calendarService.CreateAndUpdateCalerndarsByEmail(email, calendars);
            if (resoult)
                return Ok();
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid Email");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, "Nie działa");
        }
    }
}