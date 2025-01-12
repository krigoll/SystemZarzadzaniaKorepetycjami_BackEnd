using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly ITestRepository _testRepository;

    public StudentService(IStudentRepository studentRepository, ITeacherRepository teacherRepository,
        ITestRepository testRepository)
    {
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
        _testRepository = testRepository;
    }

    public async Task<List<StudentDTO>> GetStudentsThatTeachOrWillByTeachTeacherByTeacherIdAsync(int idTeacher,
        int idTest)
    {
        var teacher = await _teacherRepository.GetTeacherByIdAsync(idTeacher);
        if (teacher == null) return null;

        var test = await _testRepository.GetTestByIdAsync(idTest);
        if (test == null) return null;

        var students =
            await _studentRepository.GetStudentsThatTeachOrWillByTeachTeacherByTeacherIdAsync(idTeacher, idTest);
        return students;
    }
}