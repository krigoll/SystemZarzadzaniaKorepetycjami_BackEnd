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
            var person = await _loginRepository.findPersonByEmailAndPasswordAsync(loginDto);
            return person == null ? LoginStatus.USER_EXISTS : LoginStatus.USER_NOT_EXISTS;
        }
        catch (Exception e)
        {
            return LoginStatus.DATABASE_ERROR;
        }
    }
}