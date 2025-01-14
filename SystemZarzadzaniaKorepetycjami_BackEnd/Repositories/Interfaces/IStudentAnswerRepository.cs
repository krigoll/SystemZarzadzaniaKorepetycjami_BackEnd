using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IStudentAnswerRepository
{
    public Task CreateAndUpdateStudentAnswer(int idTestForStudent, List<StudentAnswer> studentAnswer);
    public Task<StudentAnswer> GetStudentAnswerByIdAsync(int idStudentAnswer);
}