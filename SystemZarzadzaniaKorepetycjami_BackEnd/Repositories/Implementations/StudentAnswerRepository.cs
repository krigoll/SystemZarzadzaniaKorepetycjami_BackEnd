using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations;

public class StudentAnswerRepository : IStudentAnswerRepository
{
    private readonly SZKContext _context;

    public StudentAnswerRepository(SZKContext context)
    {
        _context = context;
    }

    public async Task CreateAndUpdateStudentAnswer(int idTestForStudent, List<StudentAnswer> studentAnswers)
    {
        var assignments = await _context.Assignment
            .Where(a => a.IdTest == _context.TestForStudent
                .Where(tfs => tfs.IdTestForStudent == idTestForStudent)
                .Select(tfs => tfs.IdTest).FirstOrDefault())
            .ToListAsync();

        foreach (var assignment in assignments)
            if (!studentAnswers.Any(sa => sa.IdAssignment == assignment.IdAssignment))
            {
                var newAnswer = new StudentAnswer("Brak odpowiedzi", idTestForStudent, assignment.IdAssignment);
                studentAnswers.Add(newAnswer);
            }

        await _context.StudentAnswer.AddRangeAsync(studentAnswers);

        await _context.SaveChangesAsync();
    }


    /*public async Task CreateAndUpdateStudentAnswer(int idTestForStudent, List<StudentAnswer> studentAnswer)
    {
        foreach (var studentA in studentAnswer)
        {
            var sa = await _context.StudentAnswer.FirstOrDefaultAsync(sa =>
                sa.IdAssignment == studentA.IdAssignment && sa.IdTestForStudent == idTestForStudent);
            if (sa == null)
            {
                if (!studentA.Answer.IsNullOrEmpty()) await _context.StudentAnswer.AddAsync(studentA);
            }
            else
            {
                if (!studentA.Answer.IsNullOrEmpty())
                {
                    sa.SetAnswer(studentA.Answer);
                    _context.StudentAnswer.Update(sa);
                }
                else
                {
                    _context.StudentAnswer.Remove(sa);
                }
            }
        }

        await _context.SaveChangesAsync();
    }*/

    public Task<StudentAnswer> GetStudentAnswerByIdAsync(int idStudentAnswer)
    {
        var studentAnswer = _context.StudentAnswer.FirstOrDefaultAsync(sa => sa.IdStudentAnswer == idStudentAnswer);
        return studentAnswer;
    }
}