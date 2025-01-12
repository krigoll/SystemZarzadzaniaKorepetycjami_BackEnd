using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

    public async Task CreateAndUpdateStudentAnswer(int idTestForStudent, List<StudentAnswer> studentAnswer)
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
    }
}