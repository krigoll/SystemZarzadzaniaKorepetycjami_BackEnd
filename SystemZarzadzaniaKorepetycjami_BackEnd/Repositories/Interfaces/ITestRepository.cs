using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface ITestRepository
{
    public Task<Test> GetTestByIdAsync(int idTest);
    public Task<List<TestDTO>> GetTestByTeacherIdAsync(int teacherId);
    public Task CreateTestAsync(Test test);
    public Task UpdateTestAsync(Test test);
    public Task DeleteTestAsync(Test test);
}