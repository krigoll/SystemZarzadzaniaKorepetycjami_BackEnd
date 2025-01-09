using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

public interface ITestService
{
    public Task<TestWithAssigmentDTO> GetTestWithAssignmentsByTestIdAsync(int idTest);
    public Task<List<TestDTO>> GetTestsByTeacherIdAsync(int idTeacher);
    public Task<TestStatus> CreateTestAsync(int idTeacher, string title);
    public Task<TestStatus> UpdateTestByIdAsync(int idTest, string title);
    public Task<AssignmentToTestStatus> AddAssigmentToTestAsync(int idTest, AssignmentDTO assignmentDto);
    public Task<AssignmentToTestStatus> RemoveAssigmentFromTestAsync(int idTest, int idAssigment);
    public Task<bool> DeleteTestByIdAsync(int idTest);
}