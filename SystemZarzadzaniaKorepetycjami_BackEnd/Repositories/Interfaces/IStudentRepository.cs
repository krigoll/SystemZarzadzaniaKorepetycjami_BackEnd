using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IStudentRepository
{
    public void AddStudent(Student student);
}