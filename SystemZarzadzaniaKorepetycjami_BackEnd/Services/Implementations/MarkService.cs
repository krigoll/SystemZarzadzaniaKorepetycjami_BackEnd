using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class MarkService : IMarkService
{
    private readonly IMarkRepository _markRepository;
    private readonly IStudentAnswerRepository _studentAnswerRepository;
    private readonly ITestForStudentRepository _testForStudentRepository;

    public MarkService(IMarkRepository markRepository, ITestForStudentRepository testForStudentRepository,
        IStudentAnswerRepository studentAnswerRepository)
    {
        _markRepository = markRepository;
        _testForStudentRepository = testForStudentRepository;
        _studentAnswerRepository = studentAnswerRepository;
    }

    public async Task<MarkStatus> CreateAndUpdateMark(List<MarkDTO> marks)
    {
        try
        {
            var makr = new List<Mark>();

            foreach (var m in marks)
                makr.Add(new Mark(m.Description, m.Value, m.IdStudentAnswer));


            await _markRepository.CreateAndUpdateMark(makr);
            var studentAnswer = await _studentAnswerRepository.GetStudentAnswerByIdAsync(marks[0].IdStudentAnswer);
            var testForStudentStatusProven = 3;
            await _testForStudentRepository.ChangeStatusAsync(studentAnswer.IdTestForStudent,
                testForStudentStatusProven);

            return MarkStatus.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return MarkStatus.INVALID_MARK;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return MarkStatus.SERVER_ERROR;
        }
    }
}