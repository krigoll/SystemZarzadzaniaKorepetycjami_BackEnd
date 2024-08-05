using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IPersonRepository
{
    public Task<int> AddPerson(Person person);
    public Task<Person> FindPersonByEmailAsync(String email);
    public Task<Person> FindUserByIdAsync(int idPerson);
}