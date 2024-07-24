using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations;

public class LoginRepository : ILoginRepository
{
    private readonly SZKContext _context;

    public LoginRepository(SZKContext context)
    {
        this._context = context;
    }

    public async Task<Person?> findPersonByEmailAndPasswordAsync(LoginDTO loginDto)
    {
        return await _context.Person.FirstOrDefaultAsync(c => c.Email == loginDto.Email); // TODO Password
    }
}