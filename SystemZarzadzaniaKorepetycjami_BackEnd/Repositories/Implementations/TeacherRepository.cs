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
                .Where(p => p.Email == email && !p.IsDeleted)
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
            var tests = await _context.Test.Where(sa => sa.IdTeacher == teacher.IdTeacher).ToListAsync();
            _context.Test.RemoveRange(tests);
            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task<Teacher> GetTeacherByEmailAsync(string email)
        {
            var person = await _context.Person
                .Where(p => p.Email == email && !p.IsDeleted)
                .Select(p => new { p.IdPerson })
                .FirstOrDefaultAsync();

            return await _context.Teacher
                .FirstOrDefaultAsync(t => t.IdTeacher == person.IdPerson);
        }

        public async Task<List<TeacherDTO>> GetTeachersBySubjectCategoryAsync(int subjectLevelId, Teacher teacherE)
        {
            var teachers = await (from teacher in _context.Teacher
                join person in _context.Person on teacher.IdTeacher equals person.IdPerson
                join salary in _context.TeacherSalary on teacher.IdTeacher equals salary.IdTeacher
                join opinion in _context.Opinion on teacher.IdTeacher equals opinion.IdTeacher into opinionsGroup
                where salary.IdSubject == subjectLevelId && !person.IsDeleted && teacher.IdTeacher != teacherE.IdTeacher
                group new { person, salary, opinionsGroup } by new
                {
                    person.IdPerson,
                    person.Name,
                    person.Surname,
                    salary.HourlyRate
                }
                into grouped
                select new TeacherDTO
                {
                    IdPerson = grouped.Key.IdPerson,
                    Name = grouped.Key.Name,
                    Surname = grouped.Key.Surname,
                    HourlyRate = grouped.Key.HourlyRate,
                    Image = grouped.Select(g => g.person.Image).FirstOrDefault() == null
                        ? null
                        : Convert.ToBase64String(grouped.Select(g => g.person.Image).FirstOrDefault()),
                    AvgOpinion = grouped.SelectMany(g => g.opinionsGroup)
                        .Select(o => (float?)o.Rating)
                        .Average() ?? 0
                }).ToListAsync();

            var sortedTeachers = teachers.OrderBy(p => p.Name).ThenBy(p => p.Surname).ToList();

            return sortedTeachers;
        }

        public async Task<List<TeacherDTO>> GetTeachersBySubjectCategoryAsync(int subjectLevelId)
        {
            var teachers = await (from teacher in _context.Teacher
                join person in _context.Person on teacher.IdTeacher equals person.IdPerson
                join salary in _context.TeacherSalary on teacher.IdTeacher equals salary.IdTeacher
                join opinion in _context.Opinion on teacher.IdTeacher equals opinion.IdTeacher into opinionsGroup
                where salary.IdSubject == subjectLevelId && !person.IsDeleted
                group new { person, salary, opinionsGroup } by new
                {
                    person.IdPerson,
                    person.Name,
                    person.Surname,
                    salary.HourlyRate
                }
                into grouped
                select new TeacherDTO
                {
                    IdPerson = grouped.Key.IdPerson,
                    Name = grouped.Key.Name,
                    Surname = grouped.Key.Surname,
                    HourlyRate = grouped.Key.HourlyRate,
                    Image = grouped.Select(g => g.person.Image).FirstOrDefault() == null
                        ? null
                        : Convert.ToBase64String(grouped.Select(g => g.person.Image).FirstOrDefault()),
                    AvgOpinion = grouped.SelectMany(g => g.opinionsGroup)
                        .Select(o => (float?)o.Rating)
                        .Average() ?? 0
                }).ToListAsync();

            var sortedTeachers = teachers.OrderBy(p => p.Name).ThenBy(p => p.Surname).ToList();

            return sortedTeachers;
        }


        public async Task<Teacher> GetTeacherByIdAsync(int teacherId)
        {
            return await _context.Teacher
                .FirstOrDefaultAsync(t => t.IdTeacher == teacherId);
        }

        public async Task<List<TeacherDTO>> GetAllTeachersThatTeachStudentByStudentId(int idStudent)
        {
            var distinctData = await (
                from person in _context.Person
                join opinion in _context.Opinion on person.IdPerson equals opinion.IdTeacher into opinions
                from opinion in opinions.DefaultIfEmpty()
                join lesson in _context.Lesson on person.IdPerson equals lesson.IdTeacher into lessons
                from lesson in lessons.DefaultIfEmpty()
                join student in _context.Student on lesson.IdStudent equals student.IdStudent into students
                from student in students.DefaultIfEmpty()
                join teacher in _context.Teacher on person.IdPerson equals teacher.IdTeacher
                where !person.IsDeleted &&
                      (
                          (student != null &&
                           student.IdStudent == idStudent &&
                           lesson.IdLessonStatus == 2 &&
                           lesson.StartDate < DateTime.Now) ||
                          (opinion != null && opinion.IdStudent == idStudent)
                      )
                select new
                {
                    person.IdPerson,
                    person.Name,
                    person.Surname,
                    person.IsDeleted
                }
            ).Distinct().ToListAsync();

            var result = (
                from d in distinctData
                join person in _context.Person on d.IdPerson equals person.IdPerson
                select new TeacherDTO
                {
                    IdPerson = person.IdPerson,
                    Name = person.Name,
                    Surname = person.Surname,
                    HourlyRate = 0,
                    Image = person.Image == null ? null : Convert.ToBase64String(person.Image)
                }
            ).ToList();

            var sortedTeachers = result.OrderBy(p => p.Name).ThenBy(p => p.Surname).ToList();

            return sortedTeachers;
        }
    }
}