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

        public async Task RemoveTeacherAsync(Teacher teacher)
        {
            var teacherSalaries = await _context.TeacherSalary
                .Where(ts => ts.IdTeacher == teacher.IdTeacher)
                .ToListAsync();
            _context.TeacherSalary.RemoveRange(teacherSalaries);
            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task<Teacher> GetTeacherByEmailAsync(string email)
        {
            var person = await _context.Person
                .Where(p => p.Email == email)
                .Select(p => new { p.IdPerson })
                .FirstOrDefaultAsync();

            return await _context.Teacher
                .FirstOrDefaultAsync(t => t.IdTeacher == person.IdPerson);
        }

        public async Task<List<TeacherDTO>> GetTeachersBySubjectCategoryAsync(int subjectLevelId)
        {
            var teachers = await (from teacher in _context.Teacher
                join person in _context.Person on teacher.IdTeacher equals person.IdPerson
                join salary in _context.TeacherSalary on teacher.IdTeacher equals salary.IdTeacher
                where salary.IdSubject == subjectLevelId
                select new TeacherDTO
                {
                    IdPerson = person.IdPerson,
                    Name = person.Name,
                    Surname = person.Surname,
                    HourlyRate = salary.HourlyRate,
                    Image = person.Image == null ? null : Convert.ToBase64String(person.Image)
                }).ToListAsync();

            return teachers;
        }
    }
}