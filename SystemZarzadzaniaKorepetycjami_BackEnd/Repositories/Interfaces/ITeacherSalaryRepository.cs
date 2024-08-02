using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces
{
    public interface ITeacherSalaryRepository
    {
        public Task AddTeacherSalaryAsync(List<TeacherSalary> teacherSalaryList);
    }
}