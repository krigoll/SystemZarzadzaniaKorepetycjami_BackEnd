using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SZKContext _context;

        public TeacherRepository(SZKContext context)
        {
            _context = context;
        }

        public async Task AddTeacher(Teacher teacher)
        {
            await _context.Teacher.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> isTeacherByEmail(string email)
        {
            var person = await _context.Person
                .Where(p => p.Email == email)
                .Select(p => new { p.IdPerson })
                .FirstOrDefaultAsync();

            if (person == null) return false;

            return await _context.Teacher
                .AnyAsync(t => t.IdTeacher == person.IdPerson);
        }
    }
}