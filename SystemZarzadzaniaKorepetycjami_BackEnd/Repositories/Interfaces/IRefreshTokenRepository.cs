using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    Task StoreRefreshTokenAsync(string email, string refreshToken);
    Task<RefreshToken> GetRefreshTokenAsync(string refreshToken);
    Task ReplaceRefreshTokenAsync(string oldRefreshToken, string newRefreshToken);
    Task<string> GetUserEmailByRefreshTokenAsync(string refreshToken);
    Task<RefreshToken> GetRefreshTokenByEmailAsync(string email);
}