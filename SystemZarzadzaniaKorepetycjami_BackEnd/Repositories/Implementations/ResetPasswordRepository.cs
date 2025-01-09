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

    public async Task AddAsync(ResetPassword resetPassword)
    {
        var existingRecords = _context.ResetPassword
            .Where(rp => rp.IdPerson == resetPassword.IdPerson);

        _context.ResetPassword.RemoveRange(existingRecords);
        await _context.ResetPassword.AddAsync(resetPassword);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetIdPersonByCode(string code)
    {
        var resetPassword =
            await _context.ResetPassword.FirstOrDefaultAsync(rp => rp.Code == code && rp.ExpireDate > DateTime.Now);
        if (resetPassword == null) return -1;

        return resetPassword.IdPerson;
    }

    public async Task RemoveCode(string code)
    {
        var resetPassword = await _context.ResetPassword.FirstOrDefaultAsync(rp => rp.Code == code);
        _context.ResetPassword.Remove(resetPassword);
        await _context.SaveChangesAsync();
    }
}