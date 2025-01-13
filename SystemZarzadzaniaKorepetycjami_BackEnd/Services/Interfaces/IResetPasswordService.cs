using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

public interface IResetPasswordService
{
    public Task<CodeStatus> CreateCodeAsync(string email);
    public Task<ResetStatus> ResetPasswordAsync(string code, string password);
    public Task<ResetStatus> ResetPasswordWitOutCodeAsync(string password, int personId);
}