using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

public interface ITestForStudentService
{
    public Task<List<TestForStudentDTO>> GetGivenTestsByTeacherAsync(int idTeacher);
    public Task<List<TestForStudentDTO>> GetGivenTestsByStudentAsync(int idStudent);
    public Task<GiveTestForStudentStatus> GetGiveTestToStudentAsync(int idStudent, int idTest);
    public Task<TestForStudentDetailsDTO> GetTestForStudentDetails(int idTestForStudent);
}