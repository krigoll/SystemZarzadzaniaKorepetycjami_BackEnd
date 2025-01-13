using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
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

        public async Task<Student> GetStudentByIdAsync(int idStudent)
        {
            return await _context.Student.FirstOrDefaultAsync(s => s.IdStudent == idStudent);
        }

        public async Task<List<StudentDTO>> GetStudentsThatTeachOrWillByTeachTeacherByTeacherIdAsync(int idTeacher,
            int idTest)
        {
            var students = await (
                from lesson in _context.Lesson
                join student in _context.Student
                    on lesson.IdStudent equals student.IdStudent
                join person in _context.Person
                    on student.IdStudent equals person.IdPerson
                join testForStudent in _context.TestForStudent
                    on new { student.IdStudent, IdTest = idTest }
                    equals new { testForStudent.IdStudent, testForStudent.IdTest } into tests
                from test in tests.DefaultIfEmpty()
                where lesson.IdTeacher == idTeacher && lesson.IdLessonStatus == 2
                select new StudentDTO
                {
                    IdStudent = student.IdStudent,
                    FullName = person.Name + " " + person.Surname,
                    WasGiven = test != null
                }
            ).Distinct().ToListAsync();

            var sortedStudents = students.OrderBy(s => s.FullName).ToList();
            return sortedStudents;
        }
    }
}