using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

public interface ILoginService
{
    public Task<(LoginStatus, string, string)> LoginAsync(LoginDTO request);
    public Task<(string, string)> RefreshTokenAsync(string refreshToken);
    public string HashPassword(LoginDTO request);
}