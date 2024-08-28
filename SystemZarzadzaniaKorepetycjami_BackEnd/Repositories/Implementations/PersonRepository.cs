using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations;

public class PersonRepository : IPersonRepository
{
    private readonly SZKContext _context;

    public PersonRepository(SZKContext context)
    {
        _context = context;
    }

    public async Task<int> AddPerson(Person person)
    {
        await _context.Person.AddAsync(person);
        await _context.SaveChangesAsync();
        return person.IdPerson;
    }

    public async Task<Person> FindPersonByEmailAsync(String email)
    {
        return await _context.Person.FirstOrDefaultAsync(p => p.Email == email);
    }

    public async Task<Person> FindPersonByIdAsync(int idPerson)
    {
        return await _context.Person.FirstOrDefaultAsync(p => p.IdPerson == idPerson);
    }

    public async Task UpdateUserAsync(Person person)
    {
        _context.Person.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber)
    {
        var person = await _context.Person.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
        return person == null;
    }
}