using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _lessonRepository;
    private readonly ITeacherRepository _teacherRepository;

    public LessonService(ITeacherRepository teacherRepository, ILessonRepository lessonRepository)
    {
        _teacherRepository = teacherRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task<List<LessonDTO>> getAllReservedLessonsByTeacherEmailAsync(string email)
    {
        var teacher = await _teacherRepository.GetTeacherByEmailAsync(email);
        if (teacher == null) return null;

        return await _lessonRepository.getAllReservedLessonsByTeacherAsync(teacher);
    }
}