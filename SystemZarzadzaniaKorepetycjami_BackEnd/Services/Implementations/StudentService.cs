using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ITeacherRepository _teacherRepository;

    public StudentService(IStudentRepository studentRepository, ITeacherRepository teacherRepository)
    {
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
    }

    public async Task<List<StudentDTO>> GetStudentsThatTeachOrWillByTeachTeacherByTeacherIdAsync(int idTeacher)
    {
        var teacher = await _teacherRepository.GetTeacherByIdAsync(idTeacher);
        if (teacher == null) return null;

        var students = await _studentRepository.GetStudentsThatTeachOrWillByTeachTeacherByTeacherIdAsync(idTeacher);
        return students;
    }
}