using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IStudentRepository
{
    public Task AddStudent(Student student);
    public Task<bool> isStudentByEmail(string email);
    public Task RemoveStudentAsync(Student student);
}