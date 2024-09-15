using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

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
                                .SelectMany(sc => sc.SubjectCategory.SubjectLevel, (sc, sl) => new { Subject = sc.Name, Category = sc.SubjectCategory.Name, Level = sl.Name, LevelId = sl.IdSubjectLevel })
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
            var result = await _context.Subject
                .Include(s => s.SubjectCategory)
                .ThenInclude(sc => sc.SubjectLevel)
                .SelectMany(s => s.SubjectCategory, (s, sc) => new { s.Name, SubjectCategory = sc })
                .SelectMany(sc => sc.SubjectCategory.SubjectLevel, (sc, sl) => new { Subject = sc.Name, Category = sc.SubjectCategory.Name, Level = sl.Name, SubjectId = sc.SubjectCategory.IdSubject, LevelId = sl.IdSubjectLevel })
                .GroupJoin(_context.TeacherSalary.Where(ts => ts.IdTeacher == teacherId),
                    subject => subject.SubjectId,
                    salary => salary.IdSubject,
                    (subject, salaries) => new { subject, Salary = salaries.FirstOrDefault() })
                .Select(x => new SubjectTeacherDTO
                {
                    SubjectFullName = $"{x.subject.Subject}, {x.subject.Category}, {x.subject.Level}",
                    SubjectLevelId = x.subject.LevelId,
                    Price = x.Salary != null ? x.Salary.HourlyRate : 0
                })
                .ToListAsync();

            return result;
        }

    }
}
