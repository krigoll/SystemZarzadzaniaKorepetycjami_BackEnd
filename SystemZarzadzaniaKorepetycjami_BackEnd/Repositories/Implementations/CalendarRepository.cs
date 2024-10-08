﻿// using Microsoft.EntityFrameworkCore;
// using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
// using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
// using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
// using Task = System.Threading.Tasks.Task;

// namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations;

// public class CalendarRepository : ICalendarRepository
// {
//     private readonly SZKContext _context;

//     public CalendarRepository(SZKContext context)
//     {
//         _context = context;
//     }

//     public async Task<List<CalendarDTO>> GetCalendarsByTeacherAndRangeFromToAsync(Teacher teacher, DateTime from,
//         DateTime to)
//     {
//         return await _context.Calendar
//             .Where(c => c.IdTeacher == teacher.IdTeacher && c.StartingDate >= from && c.StartingDate <= to)
//             .Select(c => new CalendarDTO
//             {
//                 StartingDate = c.StartingDate,
//                 NumberOfLessons = c.NumberOfLessons,
//                 BreakTime = c.BreakTime
//             })
//             .ToListAsync();
//     }

//     public async Task CreateAndUpdateCalerndarsByTeacher(Teacher teacher, List<CalendarDTO> calendars)
//     {
//         foreach (var calendar in calendars)
//         {
//             var cal = await _context.Calendar.FirstOrDefaultAsync(c =>
//                 c.IdTeacher == teacher.IdTeacher && c.StartingDate == calendar.StartingDate);
//             if (cal == null)
//             {
//                 var newCalendar = new Calendar(teacher.IdTeacher, calendar.StartingDate, calendar.NumberOfLessons,
//                     calendar.BreakTime);
//                 await _context.Calendar.AddAsync(newCalendar);
//             }
//             else
//             {
//                 cal.SetNumberOfLessons(calendar.NumberOfLessons);
//                 cal.SetBreakTime(calendar.BreakTime);
//                 _context.Calendar.Update(cal);
//             }
//         }

//         await _context.SaveChangesAsync();
//     }
// }