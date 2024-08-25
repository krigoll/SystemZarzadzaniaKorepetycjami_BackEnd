using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    Task StoreRefreshTokenAsync(string email, string refreshToken);
    Task<RefreshTokens> GetRefreshTokenAsync(string refreshToken);
    Task ReplaceRefreshTokenAsync(string oldRefreshToken, string newRefreshToken);
}