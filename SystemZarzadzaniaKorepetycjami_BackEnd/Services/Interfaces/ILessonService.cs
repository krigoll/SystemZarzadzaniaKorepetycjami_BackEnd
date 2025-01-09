using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

public interface ILessonService
{
    public Task<List<LessonDTO>> getAllReservedLessonsByTeacherEmailAsync(string email);
    public Task<bool> AcceptLessonByIdAsync(int lessonId);
    public Task<bool> RejectLessonByIdAsync(int lessonId);
    public Task<bool> CancelLessonByIdAsync(int lessonId);
    public Task<LessonDatailsDTO> GetLessonDetailsByIdAsync(int lessonId);
}