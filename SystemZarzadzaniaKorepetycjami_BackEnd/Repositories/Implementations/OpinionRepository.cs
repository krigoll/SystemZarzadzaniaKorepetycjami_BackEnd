using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

public class OpinionRepository : IOpinionRepository
{
    private readonly SZKContext _context;

    public OpinionRepository(SZKContext context)
    {
        _context = context;
    }

    public async Task<List<OpinionDTO>> GetOpinionsByStudentAsync(int idStudent)
    {
        return await (from opinion in _context.Opinion
            join person in _context.Person on opinion.IdTeacher equals person.IdPerson
            where opinion.IdStudent == idStudent
            select new OpinionDTO
            {
                Rating = opinion.Rating,
                Content = opinion.Content,
                IdPerson = person.IdPerson,
                IdOpinion = opinion.IdOpinion,
                FullName = person.Name + ' ' + person.Surname
            }).ToListAsync();
    }

    public async Task<List<OpinionDTO>> GetOpinionsByTeacherAsync(int idTeacher)
    {
        return await (from opinion in _context.Opinion
            join person in _context.Person on opinion.IdStudent equals person.IdPerson
            join student in _context.Student on opinion.IdStudent equals student.IdStudent
            where opinion.IdTeacher == idTeacher
            select new OpinionDTO
            {
                Rating = opinion.Rating,
                Content = opinion.Content,
                IdPerson = person.IdPerson,
                IdOpinion = opinion.IdOpinion,
                FullName = person.Name + ' ' + person.Surname
            }).ToListAsync();
    }

    public async Task AddOpinionAsync(Opinion newOpinion)
    {
        await _context.Opinion.AddAsync(newOpinion);
        await _context.SaveChangesAsync();
    }

    public async Task<Opinion> GetOpinionByIdAsync(int opinionId)
    {
        return await _context.Opinion.FirstOrDefaultAsync(o => o.IdOpinion == opinionId);
    }

    public async Task UpdateOpinionAsync(Opinion opinion)
    {
        _context.Opinion.Update(opinion);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOpinionByIdAsync(Opinion opinion)
    {
        _context.Opinion.Remove(opinion);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOpinionsByPersonId(int idPerson)
    {
        var opinions = await _context.Opinion
            .Where(opinion => opinion.IdTeacher == idPerson || opinion.IdStudent == idPerson)
            .ToListAsync();
        _context.Opinion.RemoveRange(opinions);
        await _context.SaveChangesAsync();
    }
}