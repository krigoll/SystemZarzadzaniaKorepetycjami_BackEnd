using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations;

public class AvailabilityRepository : IAvailabilityRepository
{
    private readonly SZKContext _context;

    public AvailabilityRepository(SZKContext context)
    {
        _context = context;
    }

    public async Task<List<Availability>> GetTeacherAvailabilityByTeacherAsync(Teacher teacher)
    {
        return await _context.Availability.Where(a => a.IdTeacher == teacher.IdTeacher).ToListAsync();
    }

    public async Task CreateAndUpdateAvailabilitiesByTeacher(Teacher teacher, List<AvailabilityDTO> availabilities)
    {
        foreach (var availability in availabilities)
        {
            var ava = await _context.Availability.FirstOrDefaultAsync(a =>
                a.IdTeacher == teacher.IdTeacher && a.IdDayOfTheWeek == availability.IdDayOfTheWeek);
            if (ava == null)
            {
                if (!availability.StartTime.IsNullOrEmpty() && !availability.EndTime.IsNullOrEmpty())
                {
                    var newAvailability = new Availability(teacher.IdTeacher, availability.IdDayOfTheWeek,
                        TimeOnly.Parse(availability.StartTime), TimeOnly.Parse(availability.EndTime));
                    await _context.Availability.AddAsync(newAvailability);
                }
            }
            else
            {
                if (!availability.StartTime.IsNullOrEmpty() && !availability.EndTime.IsNullOrEmpty())
                {
                    ava.SetTime(TimeOnly.Parse(availability.StartTime), TimeOnly.Parse(availability.EndTime));
                    _context.Availability.Update(ava);
                }
                else
                {
                    _context.Availability.Remove(ava);
                }
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsThisLessonInAvailabilityTime(Lesson lesson)
    {
        var dayOfWeek = (int)lesson.StartDate.DayOfWeek;

        var teacherAvailability = await _context.Availability
            .FirstOrDefaultAsync(a => a.IdTeacher == lesson.IdTeacher && a.IdDayOfTheWeek == dayOfWeek);

        if (teacherAvailability == null)
            return false;

        var lessonStartTime = lesson.StartDate;
        var lessonEndTime = lessonStartTime.AddMinutes(lesson.DurationInMinutes);

        return lessonStartTime.TimeOfDay >= teacherAvailability.StartTime.ToTimeSpan() &&
               lessonEndTime.TimeOfDay <= teacherAvailability.EndTime.ToTimeSpan();
    }
}