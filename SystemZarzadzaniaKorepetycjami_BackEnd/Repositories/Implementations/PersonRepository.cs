using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

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
}