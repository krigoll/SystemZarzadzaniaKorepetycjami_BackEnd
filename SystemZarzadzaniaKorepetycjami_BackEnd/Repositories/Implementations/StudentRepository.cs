using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SZKContext _context;

        public StudentRepository(SZKContext context)
        {
            _context = context;
        }

        public async Task AddStudent(Student student)
        {
            await _context.Student.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> isStudentByEmail(string email)
        {
            var person = await _context.Person
                .Where(p => p.Email == email && !p.IsDeleted)
                .Select(p => new { p.IdPerson })
                .FirstOrDefaultAsync();

            if (person == null) return false;

            return await _context.Student
                .AnyAsync(s => s.IdStudent == person.IdPerson);
        }

        public async Task RemoveStudentAsync(Student student)
        {
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
        }
    
        public async Task<Student> GetStudentByEmailAsync(string email)
	    {
		    var person = await _context.Person
                    .Where(p => p.Email == email && !p.IsDeleted)
                    .Select(p => new { p.IdPerson })
                    .FirstOrDefaultAsync();
		    return await _context.Student.FirstOrDefaultAsync(s => s.IdStudent == person.IdPerson);
	    }
    }
}