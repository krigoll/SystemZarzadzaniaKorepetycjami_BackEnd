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

    public async Task<bool> AddPerson(Person person)
    {
        await _context.Person.AddAsync(person);
        var affectedRows = _context.SaveChanges();
        return affectedRows > 0;
    }
}