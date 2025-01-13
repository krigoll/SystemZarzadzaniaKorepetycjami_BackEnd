using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations;

public class TestForStudentRepository : ITestForStudentRepository
{
    private readonly SZKContext _context;

    public TestForStudentRepository(SZKContext context)
    {
        _context = context;
    }

    public async Task AddTestForStudent(TestForStudent testForStudent)
    {
        await _context.TestForStudent.AddAsync(testForStudent);
        await _context.SaveChangesAsync();
    }

    public async Task<List<TestForStudentDTO>> GetTestsForStudent(int idStudent)
    {
        var tests = await (
            from test in _context.Test
            join assignment in _context.Assignment
                on test.IdTest equals assignment.IdTest into assignments
            from assignment in assignments.DefaultIfEmpty()
            join testForStudent in _context.TestForStudent
                on test.IdTest equals testForStudent.IdTest
            join teacher in _context.Teacher
                on test.IdTeacher equals teacher.IdTeacher
            join person in _context.Person
                on teacher.IdTeacher equals person.IdPerson
            where testForStudent.IdStudent == idStudent
            group assignment by new
            {
                test.IdTest, test.Title, testForStudent.DateOfCreation,
                TeacherFullName = person.Name + " " + person.Surname,
                testForStudent.IdTestForStudent
            }
            into g
            select new TestForStudentDTO
            {
                IdTest = g.Key.IdTest,
                Title = g.Key.Title,
                NumberOfAssignments = g.Count(a => a != null),
                CreationTime = g.Key.DateOfCreation.ToString("yyyy-MM-dd HH:mm"),
                Fullname = g.Key.TeacherFullName,
                IdTestForStudent = g.Key.IdTestForStudent
            }).ToListAsync();

        var sortedTests = tests.OrderBy(t => t.Title).ThenBy(t => t.CreationTime).ToList();

        return sortedTests;
    }


    public async Task<List<TestForStudentDTO>> GetTestsForTeacher(int idTeacher)
    {
        var tests = await (
            from test in _context.Test
            join assignment in _context.Assignment
                on test.IdTest equals assignment.IdTest into assignments
            from assignment in assignments.DefaultIfEmpty()
            join testForStudent in _context.TestForStudent
                on test.IdTest equals testForStudent.IdTest
            join student in _context.Student
                on testForStudent.IdStudent equals student.IdStudent
            join person in _context.Person
                on student.IdStudent equals person.IdPerson
            where test.IdTeacher == idTeacher
            group assignment by new
            {
                test.IdTest, test.Title, testForStudent.DateOfCreation,
                StudentFullName = person.Name + " " + person.Surname,
                testForStudent.IdTestForStudent
            }
            into g
            select new TestForStudentDTO
            {
                IdTest = g.Key.IdTest,
                Title = g.Key.Title,
                NumberOfAssignments = g.Count(a => a != null),
                CreationTime = g.Key.DateOfCreation.ToString("yyyy-MM-dd HH:mm"),
                Fullname = g.Key.StudentFullName,
                IdTestForStudent = g.Key.IdTestForStudent
            }).ToListAsync();

        var sortedTests = tests.OrderBy(t => t.Title).ThenBy(t => t.CreationTime).ToList();

        return sortedTests;
    }

    public async Task<TestForStudentDetailsDTO> GetTestForStudentDetails(int idTestForStudent)
    {
        var testForStudentDetails = await (
            from tfs in _context.TestForStudent
            join t in _context.Test on tfs.IdTest equals t.IdTest
            where tfs.IdTestForStudent == idTestForStudent
            select new TestForStudentDetailsDTO
            {
                IdTestForStudent = tfs.IdTestForStudent,
                Title = t.Title,
                Assignment = (
                    from a in _context.Assignment
                    join sa in _context.StudentAnswer
                        on new { a.IdAssignment, tfs.IdTestForStudent }
                        equals new { sa.IdAssignment, sa.IdTestForStudent } into answers
                    from sa in answers.DefaultIfEmpty()
                    join m in _context.Mark on sa.IdStudentAnswer equals m.IdStudentAnswer into marks
                    from m in marks.DefaultIfEmpty()
                    where a.IdTest == tfs.IdTest
                    select new StudentAnswerAndMarkDTO
                    {
                        IdStudentAnswer = sa != null ? sa.IdStudentAnswer : 0,
                        StudentAnswer = sa != null ? sa.Answer : null,
                        IdAssignment = a.IdAssignment,
                        AnswerAssignment = a.Answer,
                        Content = a.Content,
                        IdMark = m != null ? m.IdMark : 0,
                        Description = m != null ? m.Description : null,
                        Value = m != null ? m.Value : false
                    }
                ).ToList()
            }
        ).FirstOrDefaultAsync();

        return testForStudentDetails;
    }

    public async Task<TestForStudent> GetTestForStudent(int idTestForStudent)
    {
        var testForStudent =
            await _context.TestForStudent.FirstOrDefaultAsync(tfs => tfs.IdTestForStudent == idTestForStudent);
        return testForStudent;
    }
}