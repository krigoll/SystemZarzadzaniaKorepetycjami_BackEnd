using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly SZKContext _context;

    public RefreshTokenRepository(SZKContext context)
    {
        _context = context;
    }

    public async Task StoreRefreshTokenAsync(string email, string refreshToken)
    {
        var person = await _context.Person.SingleOrDefaultAsync(p => p.Email == email && !p.IsDeleted);
        if (person == null) return;

        var token = new RefreshToken(person.IdPerson, refreshToken, DateTime.UtcNow.AddDays(7), DateTime.UtcNow);

        _context.RefreshToken.Add(token);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken> GetRefreshTokenAsync(string refreshToken)
    {
        return await _context.RefreshToken.SingleOrDefaultAsync(rt => rt.Token == refreshToken);
    }

    public async Task ReplaceRefreshTokenAsync(string oldRefreshToken, string newRefreshToken)
    {
        var token = await _context.RefreshToken.SingleOrDefaultAsync(rt => rt.Token == oldRefreshToken);
        if (token == null) return;

        token.SetToken(newRefreshToken);
        token.SetCreatedDate(DateTime.UtcNow);
        token.SetExpiryDate(DateTime.UtcNow.AddDays(7));

        await _context.SaveChangesAsync();
    }

    public async Task<string> GetUserEmailByRefreshTokenAsync(string refreshToken)
    {
        var token = await _context.RefreshToken.SingleOrDefaultAsync(rt => rt.Token == refreshToken);
        var person = await _context.Person.SingleOrDefaultAsync(p => p.IdPerson == token.IdPerson);
        return person.Email;
    }

    public async Task<RefreshToken> GetRefreshTokenByEmailAsync(string email)
    {
        var person = await _context.Person.SingleOrDefaultAsync(p => p.Email == email && !p.IsDeleted);
        return await _context.RefreshToken.SingleOrDefaultAsync(rt => rt.IdPerson == person.IdPerson);
    }
}