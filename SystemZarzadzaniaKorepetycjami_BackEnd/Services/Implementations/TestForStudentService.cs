using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class TestForStudentService : ITestForStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly ITestForStudentRepository _testForStudentRepository;
    private readonly ITestRepository _testRepository;

    public TestForStudentService(ITestForStudentRepository testForStudentRepository, ITestRepository testRepository,
        IStudentRepository studentRepository, ITeacherRepository teacherRepository)
    {
        _testForStudentRepository = testForStudentRepository;
        _testRepository = testRepository;
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
    }

    public async Task<List<TestForStudentDTO>> GetGivenTestsByTeacherAsync(int idTeacher)
    {
        var teacher = await _teacherRepository.GetTeacherByIdAsync(idTeacher);

        if (teacher == null) return null;

        return await _testForStudentRepository.GetTestsForTeacher(idTeacher);
    }

    public async Task<List<TestForStudentDTO>> GetGivenTestsByStudentAsync(int idStudent)
    {
        var student = await _studentRepository.GetStudentByIdAsync(idStudent);

        if (student == null) return null;

        return await _testForStudentRepository.GetTestsForStudent(idStudent);
    }

    public async Task<GiveTestForStudentStatus> GetGiveTestToStudentAsync(int idStudent, int idTest)
    {
        try
        {
            var student = await _studentRepository.GetStudentByIdAsync(idStudent);

            if (student == null) return GiveTestForStudentStatus.INVALID_STUDENT_ID;

            var test = await _testRepository.GetTestByIdAsync(idTest);

            if (test == null) return GiveTestForStudentStatus.INVALID_TEST_ID;

            var testForStudent = new TestForStudent(idTest, idStudent);
            await _testForStudentRepository.AddTestForStudent(testForStudent);
            return GiveTestForStudentStatus.OK;
        }
        catch (ArgumentException argumentException)
        {
            Console.WriteLine(argumentException);
            return GiveTestForStudentStatus.INVALID_TEST_FOR_STUDENT;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return GiveTestForStudentStatus.SERVER_ERROR;
        }
    }

    public async Task<TestForStudentDetailsDTO> GetTestForStudentDetails(int idTestForStudent)
    {
        return await _testForStudentRepository.GetTestForStudentDetails(idTestForStudent);
    }
}