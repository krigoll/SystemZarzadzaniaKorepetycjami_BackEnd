using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations
{
    public class LessonRepository : ILessonRepository
    {
        private readonly SZKContext _context;

        public LessonRepository(SZKContext context)
        {
            _context = context;
        }

        public async Task<bool> IsLessonConflictlessAsync(Lesson lesson)
        {
        var conflictExists = await _context.Set<Lesson>()
            .AnyAsync(l => 
                (l.IdTeacher == lesson.IdTeacher || l.IdStudent == lesson.IdStudent) &&
                (l.IdLessonStatus == 1 || l.IdLessonStatus == 2) &&
                l.StartDate < lesson.StartDate.AddMinutes(lesson.DurationInMinutes) &&
                lesson.StartDate < l.StartDate.AddMinutes(l.DurationInMinutes)
            );

        return !conflictExists;
        }
        
        public async Task AddLessonAsync(Lesson lesson)
        {
            await _context.Lesson.AddAsync(lesson);
            await _context.SaveChangesAsync();
        }

    }
}
