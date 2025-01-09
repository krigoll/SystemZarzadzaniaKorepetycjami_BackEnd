using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

public interface IStudentAnswerService
{
    public Task<StudentAnswersStatus> CreateAndUpdateStudentAnswer(int idTestForStudent,
        List<StudentAnswerDTO> studentAnswer);
}