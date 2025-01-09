using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IResetPasswordRepository
{
    public Task AddAsync(ResetPassword resetPassword);
    public Task RemoveCode(string code);
    public Task<int> GetIdPersonByCode(string code);
}