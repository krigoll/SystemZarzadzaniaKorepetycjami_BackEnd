using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations;

public class MarkRepository : IMarkRepository
{
    private readonly SZKContext _context;

    public MarkRepository(SZKContext context)
    {
        _context = context;
    }

    public async Task CreateAndUpdateMark(List<Mark> marks)
    {
        foreach (var mark in marks)
        {
            var m = await _context.Mark.FirstOrDefaultAsync(m =>
                m.IdStudentAnswer == mark.IdStudentAnswer);
            if (m == null)
            {
                await _context.Mark.AddAsync(mark);
            }
            else
            {
                m.SetDescription(mark.Description);
                m.SetValue(mark.Value);
                _context.Mark.Update(m);
            }
        }

        await _context.SaveChangesAsync();
    }
}