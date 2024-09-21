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

        var token = new RefreshTokens(person.IdPerson, refreshToken, DateTime.UtcNow.AddDays(7), DateTime.UtcNow);

        _context.RefreshTokens.Add(token);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshTokens> GetRefreshTokenAsync(string refreshToken)
    {
        return await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == refreshToken);
    }

    public async Task ReplaceRefreshTokenAsync(string oldRefreshToken, string newRefreshToken)
    {
        var token = await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == oldRefreshToken);
        if (token == null) return;

        token.SetToken(newRefreshToken);
        token.SetCreatedDate(DateTime.UtcNow);
        token.SetExpiryDate(DateTime.UtcNow.AddDays(7));

        await _context.SaveChangesAsync();
    }

    public async Task<string> GetUserEmailByRefreshTokenAsync(string refreshToken)
    {
        var token = await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == refreshToken);
        var person = await _context.Person.SingleOrDefaultAsync(p => p.IdPerson == token.IdPerson);
        return person.Email;
    }

    public async Task<RefreshTokens> GetRefreshTokenByEmailAsync(string email)
    {
        var person = await _context.Person.SingleOrDefaultAsync(p => p.Email == email);
        return await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.IdPerson == person.IdPerson);
    }
}