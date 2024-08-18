using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations;

public class AvailabilityRepository : IAvailabilityRepository
{
    private readonly SZKContext _context;

    public AvailabilityRepository(SZKContext context)
    {
        _context = context;
    }

    public async Task<List<Availability>> GetTeacherAvailabilityByTeacherAsync(Teacher teacher)
    {
        return await _context.Availability.Where(a => a.IdTeacher == teacher.IdTeacher).ToListAsync();
    }
}