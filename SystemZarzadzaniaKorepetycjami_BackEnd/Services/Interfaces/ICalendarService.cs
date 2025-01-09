using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces
{
    public interface ICalendarService
    {
        public Task<List<List<CalendarDTO>>> GetCalendarsFromTheWeekByDateAndTeacherEmail(DateOnly dateOnly, string email);
        public Task<List<List<CalendarDTO>>> GetCalendarsFromTheWeekByDate(DateOnly dateOnly);
    }
}
