using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IResetPasswordRepository
{
    public Task AddAsync(RessetPassword ressetPassword);
    public Task<int> GetIdPersonByCode(string code);
}