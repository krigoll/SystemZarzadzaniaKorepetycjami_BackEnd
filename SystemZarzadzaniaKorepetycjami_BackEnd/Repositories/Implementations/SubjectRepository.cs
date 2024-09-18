using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SZKContext _context;

        public SubjectRepository(SZKContext context)
        {
            _context = context;
        }

        public async Task<List<SubjectDTO>> GetAllFullSubjects()
        {
            var result = await _context.Subject
                .Include(s => s.SubjectCategory)
                .ThenInclude(sc => sc.SubjectLevel)
                .SelectMany(s => s.SubjectCategory, (s, sc) => new { s.Name, SubjectCategory = sc })
                .SelectMany(sc => sc.SubjectCategory.SubjectLevel,
                    (sc, sl) => new
                    {
                        Subject = sc.Name, Category = sc.SubjectCategory.Name, Level = sl.Name,
                        LevelId = sl.IdSubjectLevel
                    })
                .ToListAsync();

            var formattedResult = result.Select(x => new SubjectDTO
                {
                    SubjectFullName = $"{x.Subject}, {x.Category}, {x.Level}",
                    SubjectLevelId = x.LevelId
                })
                .ToList();

            return formattedResult;
        }

        public async Task<List<SubjectTeacherDTO>> GetAllFullSubjectsByTeacherId(int teacherId)
        {
            var result = await _context.SubjectLevel
                .Join(_context.SubjectCategory,
                    sl => sl.IdSubjectCategory,
                    sc => sc.IdSubjectCategory,
                    (sl, sc) => new { SubjectLevel = sl, SubjectCategory = sc })
                .Join(_context.Subject,
                    sc => sc.SubjectCategory.IdSubject,
                    s => s.IdSubject,
                    (sc, s) => new { sc.SubjectLevel, sc.SubjectCategory, Subject = s })
                .GroupJoin(_context.TeacherSalary.Where(ts => ts.IdTeacher == teacherId),
                    subject => subject.SubjectLevel.IdSubjectLevel, // Powiązanie SubjectLevel
                    salary => salary.IdSubject, // Powiązanie z TeacherSalary na podstawie SubjectLevel
                    (subject, salaries) => new { subject, Salary = salaries.FirstOrDefault() }) // LEFT JOIN
                .Select(x => new SubjectTeacherDTO
                {
                    SubjectFullName =
                        $"{x.subject.Subject.Name}, {x.subject.SubjectCategory.Name}, {x.subject.SubjectLevel.Name}",
                    SubjectLevelId = x.subject.SubjectLevel.IdSubjectLevel,
                    Price = x.Salary != null ? x.Salary.HourlyRate : 0
                })
                .ToListAsync();

            return result;
        }
    }
}