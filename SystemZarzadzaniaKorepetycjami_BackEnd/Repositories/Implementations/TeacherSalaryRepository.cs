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
    }
}