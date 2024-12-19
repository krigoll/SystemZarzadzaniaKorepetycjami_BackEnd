using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class TestService : ITestService
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly ITestRepository _testRepository;

    public TestService(IAssignmentRepository assignmentRepository, ITestRepository testRepository,
        ITeacherRepository teacherRepository)
    {
        _assignmentRepository = assignmentRepository;
        _testRepository = testRepository;
        _teacherRepository = teacherRepository;
    }

    public async Task<TestWithAssigmentDTO> GetTestWithAssignmentsByTestIdAsync(int idTest)
    {
        var test = await _testRepository.GetTestByIdAsync(idTest);
        if (test == null) return null;

        var assignments = await _assignmentRepository.GetAssignmentsByTestIdAsync(idTest);

        return new TestWithAssigmentDTO
        {
            IdTest = idTest,
            Title = test.Title,
            Assigments = assignments
        };
    }

    public async Task<List<TestDTO>> GetTestsByTeacherIdAsync(int idTeacher)
    {
        var teacher = await _teacherRepository.GetTeacherByIdAsync(idTeacher);
        if (teacher == null) return null;

        return await _testRepository.GetTestByTeacherIdAsync(idTeacher);
    }

    public async Task<TestStatus> CreateTestAsync(int idTeacher, string title)
    {
        try
        {
            var teacher = await _teacherRepository.GetTeacherByIdAsync(idTeacher);
            if (teacher == null) return TestStatus.INVALID_TEACHER_ID;

            var test = new Test(idTeacher, title);
            await _testRepository.CreateTestAsync(test);
            return TestStatus.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return TestStatus.INVALID_TEST;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return TestStatus.SERVER_ERROR;
        }
    }

    public async Task<TestStatus> UpdateTestByIdAsync(int idTest, string title)
    {
        try
        {
            var test = await _testRepository.GetTestByIdAsync(idTest);
            if (test == null) return TestStatus.INVALID_TEST_ID;

            test.SetTitle(title);

            await _testRepository.UpdateTestAsync(test);
            return TestStatus.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return TestStatus.INVALID_TEST;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return TestStatus.SERVER_ERROR;
        }
    }

    public async Task<AssignmentToTestStatus> AddAssigmentToTestAsync(int idTest, AssignmentDTO assignmentDto)
    {
        try
        {
            var test = await _testRepository.GetTestByIdAsync(idTest);
            if (test == null) return AssignmentToTestStatus.INVALID_TEST_ID;

            var assigment = new Assignment(assignmentDto.Content, assignmentDto.Answer, idTest);

            await _assignmentRepository.CreateAssignmentAsync(assigment);
            return AssignmentToTestStatus.OK;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return AssignmentToTestStatus.SERVER_ERROR;
        }
    }

    public async Task<AssignmentToTestStatus> RemoveAssigmentFromTestAsync(int idTest, int idAssignment)
    {
        try
        {
            var test = await _testRepository.GetTestByIdAsync(idTest);
            if (test == null) return AssignmentToTestStatus.INVALID_TEST_ID;

            var assignment = await _assignmentRepository.GetAssignmentByIdAsync(idAssignment);
            if (assignment == null) return AssignmentToTestStatus.INVALID_ASSIGMENT_ID;

            if (assignment.IdTest != test.IdTest) return AssignmentToTestStatus.ASSIGMENT_NOT_ON_THAT_TEST;

            await _assignmentRepository.DeleteAssignmentAsync(assignment);
            return AssignmentToTestStatus.OK;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return AssignmentToTestStatus.SERVER_ERROR;
        }
    }

    public async Task<bool> DeleteTestByIdAsync(int idTest)
    {
        var test = await _testRepository.GetTestByIdAsync(idTest);
        if (test == null) return false;

        await _testRepository.DeleteTestAsync(test);
        return true;
    }
}