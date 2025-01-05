using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations;

public class ResetPasswordRepository : IResetPasswordRepository
{
    private readonly SZKContext _context;

    public ResetPasswordRepository(SZKContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RessetPassword ressetPassword)
    {
        var existingRecords = _context.RessetPassword
            .Where(rp => rp.IdPerson == ressetPassword.IdPerson);

        _context.RessetPassword.RemoveRange(existingRecords);
        await _context.RessetPassword.AddAsync(ressetPassword);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetIdPersonByCode(string code)
    {
        var resetPassword =
            await _context.RessetPassword.FirstOrDefaultAsync(rp => rp.Code == code && rp.ExpiryDate > DateTime.Now);
        if (resetPassword == null) return -1;

        return resetPassword.IdPerson;
    }

    public async Task RemoveCode(string code)
    {
        var resetPassword = await _context.RessetPassword.FirstOrDefaultAsync(rp => rp.Code == code);
        _context.RessetPassword.Remove(resetPassword);
        await _context.SaveChangesAsync();
    }
}