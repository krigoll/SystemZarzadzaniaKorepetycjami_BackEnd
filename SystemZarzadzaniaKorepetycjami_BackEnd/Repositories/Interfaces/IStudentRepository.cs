using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IStudentRepository
{
    public Task AddStudent(Student student);
    public Task<bool> isStudentByEmail(string email);
    public Task RemoveStudentAsync(Student student);
    public Task<Student> GetStudentByEmailAsync(string email);
    public Task<Student> GetStudentByIdAsync(int idStudent);
    public Task<List<StudentDTO>> GetStudentsThatTeachOrWillByTeachTeacherByTeacherIdAsync(int idTeacher, int idTest);
}