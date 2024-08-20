using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        public Task AddTeacher(Teacher teacher);
        public Task<bool> isTeacherByEmail(string email);
        public Task RemoveTeacherAsync(Teacher teacher);

        public Task<Teacher> GetTeacherByEmailAsync(string email);
    }
}