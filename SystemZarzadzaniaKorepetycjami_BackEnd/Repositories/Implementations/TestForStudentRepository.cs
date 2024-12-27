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
                TeacherFullName = person.Name + " " + person.Surname
            }
            into g
            select new TestForStudentDTO
            {
                IdTest = g.Key.IdTest,
                Title = g.Key.Title,
                NumberOfAssignments = g.Count(a => a != null),
                CreationTime = g.Key.DateOfCreation,
                Fullname = g.Key.TeacherFullName
            }).ToListAsync();

        return tests;
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
                StudentFullName = person.Name + " " + person.Surname
            }
            into g
            select new TestForStudentDTO
            {
                IdTest = g.Key.IdTest,
                Title = g.Key.Title,
                NumberOfAssignments = g.Count(a => a != null),
                CreationTime = g.Key.DateOfCreation,
                Fullname = g.Key.StudentFullName
            }).ToListAsync();

        return tests;
    }
}