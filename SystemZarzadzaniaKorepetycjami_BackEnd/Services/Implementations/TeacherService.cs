using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class TeacherService : ITeacherService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ITeacherRepository _teacherRepository;

    public TeacherService(ITeacherRepository teacherRepository, IStudentRepository studentRepository)
    {
        _teacherRepository = teacherRepository;
        _studentRepository = studentRepository;
    }

    public async Task<List<TeacherDTO>> GetTeachersBySubjectCategoryAsync(int subjectLevelId, string email)
    {
        var teacher = await _teacherRepository.GetTeacherByEmailAsync(email);
        if (teacher == null)
            return await _teacherRepository.GetTeachersBySubjectCategoryAsync(subjectLevelId);
        return await _teacherRepository.GetTeachersBySubjectCategoryAsync(subjectLevelId, teacher);
    }

    public async Task<List<TeacherDTO>> GetAllTeachersThatTeachStudentByStudentEmail(string studentEmail)
    {
        var student = await _studentRepository.GetStudentByEmailAsync(studentEmail);
        if (student == null) return null;

        var teachers = await _teacherRepository.GetAllTeachersThatTeachStudentByStudentId(student.IdStudent);
        return teachers;
    }
}