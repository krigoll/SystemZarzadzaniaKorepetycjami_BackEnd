using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface ILoginRepository
{
    public Task<Person> findPersonByEmailAndPasswordAsync(LoginDTO loginDto);
}