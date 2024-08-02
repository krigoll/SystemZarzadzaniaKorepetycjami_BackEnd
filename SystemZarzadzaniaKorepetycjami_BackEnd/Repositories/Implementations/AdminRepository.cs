using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations;

public class AdminRepository : IAdminRepository
{
    private readonly SZKContext _context;

    public AdminRepository(SZKContext context)
    {
        _context = context;
    }

    public async Task<bool> isAdministratorByEmail(string email)
    {
        var person = await _context.Person
            .Where(p => p.Email == email)
            .Select(p => new { p.IdPerson })
            .FirstOrDefaultAsync();

        if (person == null) return false;

        return await _context.Administrator
            .AnyAsync(a => a.IdAdministrator == person.IdPerson);
    }
}