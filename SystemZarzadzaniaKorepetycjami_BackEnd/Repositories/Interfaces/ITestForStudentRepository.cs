using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface ITestForStudentRepository
{
    public Task AddTestForStudent(TestForStudent testForStudent);
    public Task<List<TestForStudentDTO>> GetTestsForStudent(int idStudent);
    public Task<List<TestForStudentDTO>> GetTestsForTeacher(int idTeacher);
    public Task<TestForStudentDetailsDTO> GetTestForStudentDetails(int idTestForStudent);
    public Task<TestForStudent> GetTestForStudentAsync(int idTestForStudent);
    public Task ChangeStatusAsync(int idTestForStudent, int idTestForStudentStatus);
}