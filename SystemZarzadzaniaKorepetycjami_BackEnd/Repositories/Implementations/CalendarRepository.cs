using Microsoft.EntityFrameworkCore;
using System.Globalization;
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

    public async Task<List<List<CalendarDTO>>> GetCalendarsByPersonAndRangeFromToAsync(Person person, DateTime from,
        DateTime to)
    {
        var calendarsOfTheWeek = new List<List<CalendarDTO>>();
        for (var i = from; i <= to; i = i.AddDays(1))
        {
            var calendarsOfTheDay = await (from lesson in _context.Lesson
                join lessonStatus in _context.LessonStatus on lesson.IdLessonStatus equals lessonStatus.IdLessonStatus
                join subjectLevel in _context.SubjectLevel on lesson.IdSubjectLevel equals subjectLevel.IdSubjectLevel
                join subjectCategory in _context.SubjectCategory on subjectLevel.IdSubjectCategory equals
                    subjectCategory.IdSubjectCategory
                join subject in _context.Subject on subjectCategory.IdSubject equals subject.IdSubject
                where (lesson.IdTeacher == person.IdPerson || lesson.IdStudent == person.IdPerson) &&
                      lesson.StartDate.Date == i.Date
                select new CalendarDTO
                {
                    LessonId = lesson.IdLesson,
                    DateTime = lesson.StartDate.ToString("dd.MM.yyyy HH:mm", new CultureInfo("pl-PL")),
                    SubjectName = $"{subject.Name}, {subjectCategory.Name}, {subjectLevel.Name}",
                    StatusName = lessonStatus.Status
                }).ToListAsync();
            Console.WriteLine(calendarsOfTheDay);
            calendarsOfTheWeek.Add(calendarsOfTheDay);
        }

        return calendarsOfTheWeek;
    }

    public async Task<List<List<CalendarDTO>>> GetCalendarsRangeFromToAsync(DateTime from,
    DateTime to)
{
    var calendarsOfTheWeek = new List<List<CalendarDTO>>();

    for (var i = from; i <= to; i = i.AddDays(1))
    {
        var calendarsOfTheDay = await (from lesson in _context.Lesson
            join lessonStatus in _context.LessonStatus on lesson.IdLessonStatus equals lessonStatus.IdLessonStatus
            join subjectLevel in _context.SubjectLevel on lesson.IdSubjectLevel equals subjectLevel.IdSubjectLevel
            join subjectCategory in _context.SubjectCategory on subjectLevel.IdSubjectCategory equals subjectCategory.IdSubjectCategory
            join subject in _context.Subject on subjectCategory.IdSubject equals subject.IdSubject
            where lesson.StartDate.Date == i.Date
            select new CalendarDTO
            {
                LessonId = lesson.IdLesson,
                DateTime = lesson.StartDate.ToString("dd.MM.yyyy HH:mm", new CultureInfo("pl-PL")),
                SubjectName = $"{subject.Name}, {subjectCategory.Name}, {subjectLevel.Name}",
                StatusName = lessonStatus.Status
            }).ToListAsync();

        calendarsOfTheWeek.Add(calendarsOfTheDay);
    }

    return calendarsOfTheWeek;
}
}