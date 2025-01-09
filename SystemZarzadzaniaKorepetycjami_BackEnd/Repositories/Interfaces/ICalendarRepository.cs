using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface ICalendarRepository
{
    public Task<List<List<CalendarDTO>>> GetCalendarsByPersonAndRangeFromToAsync(Person person, DateTime from, DateTime to);
    public Task<List<List<CalendarDTO>>> GetCalendarsRangeFromToAsync(DateTime from, DateTime to);
}