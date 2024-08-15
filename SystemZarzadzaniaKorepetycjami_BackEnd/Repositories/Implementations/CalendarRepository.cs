using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations;

public class CalendarRepository : ICalendarRepository
{
    private readonly SZKContext _context;

    public CalendarRepository(SZKContext context)
    {
        _context = context;
    }

    public async Task<List<CalendarDTO>> GetCalendarsByTeacherAndRangeFromToAsync(Teacher teacher, DateTime from,
        DateTime to)
    {
        var calendars = await _context.Calendar
            .Where(c => c.IdTeacher == teacher.IdTeacher && c.Date >= from && c.Date <= to)
            .Select(c => new CalendarDTO
            {
                StartingDate = c.Date,
                NumberOfLessons = c.NumberOfLessons,
                BreakTime = c.BreakTime
            })
            .ToListAsync();
        return calendars;
    }
}