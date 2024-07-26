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
            var person = await _loginRepository.findPersonByEmailAsync(loginDto.Email);
            if (person == null) return LoginStatus.USER_NOT_EXISTS;

            if (BCrypt.Net.BCrypt.Verify(loginDto.Password, person.Password)) return LoginStatus.USER_EXISTS;

            return LoginStatus.USER_NOT_EXISTS;
        }
        catch (Exception e)
        {
            return LoginStatus.DATABASE_ERROR;
        }
    }
}