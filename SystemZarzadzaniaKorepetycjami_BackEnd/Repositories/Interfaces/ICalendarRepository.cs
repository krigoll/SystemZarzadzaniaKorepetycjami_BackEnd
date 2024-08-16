using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface ICalendarRepository
{
    public Task<List<CalendarDTO>>
        GetCalendarsByTeacherAndRangeFromToAsync(Teacher teacher, DateTime from, DateTime to);
    public Task CreateAndUpdateCalerndarsByTeacher(Teacher teacher, List<CalendarDTO> calendars);
}