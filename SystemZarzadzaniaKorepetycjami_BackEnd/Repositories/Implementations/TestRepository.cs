using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations;

public class TestRepository : ITestRepository
{
    private readonly SZKContext _context;

    public TestRepository(SZKContext context)
    {
        _context = context;
    }

    public async Task<Test> GetTestByIdAsync(int idTest)
    {
        var test = await _context.Test.FirstOrDefaultAsync(t => t.IdTest == idTest);
        return test;
    }

    public async Task<List<TestDTO>> GetTestByTeacherIdAsync(int teacherId)
    {
        var tests = await (
            from test in _context.Test
            join assignment in _context.Assignment
                on test.IdTest equals assignment.IdTest into assignments
            from assignment in assignments.DefaultIfEmpty()
            where test.IdTeacher == teacherId
            group assignment by new { test.IdTest, test.Title }
            into g
            select new TestDTO
            {
                IdTest = g.Key.IdTest,
                Title = g.Key.Title,
                NumberOfAssignments = g.Count(a => a != null)
            }).ToListAsync();

        return tests;
    }

    public async Task CreateTestAsync(Test test)
    {
        await _context.Test.AddAsync(test);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTestAsync(Test test)
    {
        _context.Test.Update(test);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTestAsync(Test test)
    {
        var assignments = _context.Assignment.Where(a => a.IdTest == test.IdTest);
        _context.Assignment.RemoveRange(assignments);
        _context.Test.Remove(test);
        await _context.SaveChangesAsync();
    }
}