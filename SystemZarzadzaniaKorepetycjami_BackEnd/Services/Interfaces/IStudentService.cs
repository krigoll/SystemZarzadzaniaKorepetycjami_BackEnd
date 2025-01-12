using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

public interface IStudentService
{
    public Task<List<StudentDTO>> GetStudentsThatTeachOrWillByTeachTeacherByTeacherIdAsync(int idTeacher, int idTest);
}