using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class StudentAnswerService : IStudentAnswerService
{
    private readonly IStudentAnswerRepository _studentAnswerRepository;
    private readonly ITestForStudentRepository _testForStudentRepository;

    public StudentAnswerService(ITestForStudentRepository testForStudentRepository,
        IStudentAnswerRepository studentAnswerRepository)
    {
        _testForStudentRepository = testForStudentRepository;
        _studentAnswerRepository = studentAnswerRepository;
    }

    public async Task<StudentAnswersStatus> CreateAndUpdateStudentAnswer(int idTestForStudent,
        List<StudentAnswerDTO> studentAnswer)
    {
        try
        {
            var testForStudent = await _testForStudentRepository.GetTestsForStudent(idTestForStudent);

            if (testForStudent == null) return StudentAnswersStatus.INVALID_IT_TEST_FOR_STUDENT;

            var studentA = new List<StudentAnswer>();

            foreach (var sa in studentAnswer)
                studentA.Add(new StudentAnswer(sa.Answer, idTestForStudent, sa.IdAssignment));

            await _studentAnswerRepository.CreateAndUpdateStudentAnswer(idTestForStudent, studentA);

            return StudentAnswersStatus.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return StudentAnswersStatus.INVALID_STUDENT_ANSWER;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StudentAnswersStatus.SERVER_ERROR;
        }
    }
}