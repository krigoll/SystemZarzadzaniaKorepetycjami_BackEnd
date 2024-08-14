using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface ICalendarRepository
{
    public Task<List<CalendarDTO>>
        GetCalendarsByTeacherAndRangeFromToAsync(Teacher teacher, DateTime from, DateTime to);
}