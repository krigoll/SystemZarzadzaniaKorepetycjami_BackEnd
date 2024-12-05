using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations
{
    public class TeacherSalaryRepository : ITeacherSalaryRepository
    {
        private readonly SZKContext _context;

        public TeacherSalaryRepository(SZKContext context)
        {
            _context = context;
        }

        public async Task AddTeacherSalaryAsync(List<TeacherSalary> teacherSalaryList)
        {
            await _context.TeacherSalary.AddRangeAsync(teacherSalaryList);
            await _context.SaveChangesAsync();
        }

        public async Task<TeacherSalary> FindByTeacherAndSubjectAsync(int teacherId, int subjectLevelId)
        {
            return await _context.TeacherSalary
                .FirstOrDefaultAsync(ts => ts.IdTeacher == teacherId && ts.IdTeacherSalary == subjectLevelId);
        }

        public async Task UpdateTeacherSalaryAsync(TeacherSalary teacherSalary)
        {
            _context.TeacherSalary.Update(teacherSalary);
            await _context.SaveChangesAsync();
        }

        public async Task CreateUpdateDeleteTeacherSalaryByPersonAsync(List<TeacherSalaryDTO> teacherSalaryDTO,
            Person person)
        {
            Console.WriteLine(teacherSalaryDTO.Count);
            foreach (var teacherSalary in teacherSalaryDTO)
            {
                var ts = await _context.TeacherSalary.FirstOrDefaultAsync(t =>
                    t.IdTeacher == person.IdPerson && t.IdSubject == teacherSalary.Subject_LevelId);
                if (ts == null)
                {
                    if (teacherSalary.HourlyRate > 0)
                    {
                        var newTeacherSalary = new TeacherSalary(teacherSalary.HourlyRate, person.IdPerson,
                            teacherSalary.Subject_LevelId);
                        await _context.TeacherSalary.AddAsync(newTeacherSalary);
                    }
                }
                else
                {
                    if (teacherSalary.HourlyRate > 0)
                    {
                        ts.SetHourlyRate(teacherSalary.HourlyRate);
                        _context.TeacherSalary.Update(ts);
                    }
                    else
                    {
                        _context.TeacherSalary.Remove(ts);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}