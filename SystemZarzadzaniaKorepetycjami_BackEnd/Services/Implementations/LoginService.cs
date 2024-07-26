using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class LoginService : ILoginService
{
    private readonly ILoginRepository _loginRepository;

    public LoginService(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    public async Task<LoginStatus> LoginUserByEmailAndPasswordAsync(LoginDTO loginDto)
    {
        try
        {
            int workFactor = 12;
            loginDto.Password = BCrypt.Net.BCrypt.HashPassword(loginDto.Password, workFactor);
            var person = await _loginRepository.findPersonByEmailAndPasswordAsync(loginDto);
            return person == null ? LoginStatus.USER_NOT_EXISTS : LoginStatus.USER_EXISTS;
        }
        catch (Exception e)
        {
            return LoginStatus.DATABASE_ERROR;
        }
    }
}