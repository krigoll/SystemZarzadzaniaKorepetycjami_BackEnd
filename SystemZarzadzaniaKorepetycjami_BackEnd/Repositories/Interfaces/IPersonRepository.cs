using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IPersonRepository
{
    public Task<int> AddPerson(Person person);
    public Task<Person> FindPersonByEmailAsync(String email);
    public Task<Person> FindPersonByIdAsync(int idPerson);
    public Task UpdateUserAsync(Person person);
    public Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber);
}