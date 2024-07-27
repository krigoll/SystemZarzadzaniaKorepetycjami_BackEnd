using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SZKContext _context;

        public StudentRepository(SZKContext context)
        {
            _context = context;
        }

        public async void AddStudent(Student student)
        {
            await _context.Student.AddAsync(student);
            await _context.SaveChangesAsync();
        }
    }
}