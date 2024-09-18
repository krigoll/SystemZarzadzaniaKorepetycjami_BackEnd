using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces
{
    public interface ITeacherSalaryRepository
    {
        public Task AddTeacherSalaryAsync(List<TeacherSalary> teacherSalaryList);
        public Task<TeacherSalary> FindByTeacherAndSubjectAsync(int teacherId, int subjectLevelId);
        public Task UpdateTeacherSalaryAsync(TeacherSalary teacherSalary);
        public Task CreateUpdateDeleteTeacherSalaryByPersonAsync(List<TeacherSalaryDTO> teacherSalaryDTO, Person person);
    }
}