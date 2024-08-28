using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

public interface ILessonService
{
    public Task<List<LessonDTO>> getAllReservedLessonsByTeacherEmailAsync(string email);
}