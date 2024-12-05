using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class CalendarService : ICalendarService
{
    private readonly ICalendarRepository _calendarRepository;
    private readonly IPersonRepository _personRepostiory;

    public CalendarService(ICalendarRepository calendarRepository, IPersonRepository personRepostiory)
    {
        _calendarRepository = calendarRepository;
        _personRepostiory = personRepostiory;
    }

    public async Task<List<List<CalendarDTO>>> GetCalendarsFromTheWeekByDateAndTeacherEmail(DateOnly dateOnly,
        string email)
    {
        (var startOfWeek, var endOfWeek) = GetStartAndEndOfWeek(dateOnly);

        if (startOfWeek > endOfWeek)
        {
            return new List<List<CalendarDTO>>();
        }

        var person = await _personRepostiory.FindPersonByEmailAsync(email);
        if (person == null)
        {
            return null;
        }

        var calendars =
            await _calendarRepository.GetCalendarsByPersonAndRangeFromToAsync(person, startOfWeek, endOfWeek);
        return calendars;
    }

    public static (DateTime startOfWeek, DateTime endOfWeek) GetStartAndEndOfWeek(DateOnly date)
    {
        var daysToStartOfWeek = (int)date.DayOfWeek - (int)DayOfWeek.Monday;
        if (daysToStartOfWeek < 0)
        {
            daysToStartOfWeek += 7;
        }

        var startOfWeek = date.AddDays(-daysToStartOfWeek);
        var endOfWeek = startOfWeek.AddDays(6);

        return (startOfWeek.ToDateTime(TimeOnly.MinValue), endOfWeek.ToDateTime(TimeOnly.MinValue));
    }
}