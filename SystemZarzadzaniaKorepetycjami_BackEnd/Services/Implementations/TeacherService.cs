using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;

    public TeacherService(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<List<TeacherDTO>> GetTeachersBySubjectCategoryAsync(int subjectLevelId, string email)
    {
        var teacher = await _teacherRepository.GetTeacherByEmailAsync(email);
        if(teacher == null)
            return await _teacherRepository.GetTeachersBySubjectCategoryAsync(subjectLevelId);
        return await _teacherRepository.GetTeachersBySubjectCategoryAsync(subjectLevelId, teacher);
    }
}