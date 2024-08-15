using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class CalendarService : ICalendarService
{
    private readonly ICalendarRepository _calendarRepository;
    private readonly ITeacherRepository _teacherRepository;

    public CalendarService(ICalendarRepository calendarRepository, ITeacherRepository teacherRepository)
    {
        _calendarRepository = calendarRepository;
        _teacherRepository = teacherRepository;
    }

    public async Task<List<CalendarDTO>> GetCalendarsFromTheWeekByDateAndTeacherEmail(DateOnly dateOnly, string email)
    {
        (var startOfWeek, var endOfWeek) = GetStartAndEndOfWeek(dateOnly);

        if (startOfWeek < DateTime.Today) startOfWeek = DateTime.Today;

        if (startOfWeek > endOfWeek) return new List<CalendarDTO>();

        var teacher = await _teacherRepository.GetTeacherByEmailAsync(email);
        if (teacher == null) return null;

        var calendars =
            await _calendarRepository.GetCalendarsByTeacherAndRangeFromToAsync(teacher, startOfWeek, endOfWeek);
        var difference = endOfWeek - startOfWeek;
        var daysBetween = difference.Days + 1;
        if (daysBetween == calendars.Count) return calendars;

        var newCalendarsToReturn = new List<CalendarDTO>();
        for (var i = 0; i < daysBetween; i++)
        {
            var targetTime = startOfWeek.AddDays(i);
            var result = calendars.FirstOrDefault(c => c.StartingDate.Date == targetTime);
            if (result != null)
                newCalendarsToReturn.Add(result);
            else
                newCalendarsToReturn.Add(new CalendarDTO
                {
                    StartingDate = targetTime,
                    NumberOfLessons = 0,
                    BreakTime = 0
                });
        }

        return newCalendarsToReturn;
    }

    public static (DateTime startOfWeek, DateTime endOfWeek) GetStartAndEndOfWeek(DateOnly date)
    {
        var daysToStartOfWeek = (int)date.DayOfWeek - (int)DayOfWeek.Monday;
        if (daysToStartOfWeek < 0) daysToStartOfWeek += 7;

        var startOfWeek = date.AddDays(-daysToStartOfWeek);
        var endOfWeek = startOfWeek.AddDays(6);

        return (startOfWeek.ToDateTime(TimeOnly.MinValue), endOfWeek.ToDateTime(TimeOnly.MinValue));
    }
}