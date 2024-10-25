using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
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
        return await _context.Person.FirstOrDefaultAsync(p => p.Email == email && !p.IsDeleted);
    }

    public async Task<Person> FindPersonByIdAsync(int idPerson)
    {
        return await _context.Person.FirstOrDefaultAsync(p => p.IdPerson == idPerson && !p.IsDeleted);
    }

    public async Task UpdateUserAsync(Person person)
    {
        _context.Person.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber)
    {
        var person = await _context.Person.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber && !p.IsDeleted);
        return person == null;
    }

    public async Task DeleteUserAsync(Person person)
    {
        person.SetIsDeleted(true);
        _context.Person.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task<List<PersonDTO>> FindPersonBySearchAsync(string search)
    {
        var searchTerms = search.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        IQueryable<Person> query = _context.Person.Where(p => !p.IsDeleted);

        if (searchTerms.Length == 1)
        {
            var term = searchTerms[0];
            query = query.Where(p => p.Name.Contains(term) || p.Surname.Contains(term));
        }
        else if (searchTerms.Length >= 2)
        {
            var firstTerm = searchTerms[0];
            var secondTerm = searchTerms[1];
            query = query.Where(p =>
                (p.Name.Contains(firstTerm) && p.Surname.Contains(secondTerm)) ||
                (p.Name.Contains(secondTerm) && p.Surname.Contains(firstTerm)));
        }

        return await query.Select(person => new PersonDTO
        {
            IdPerson = person.IdPerson,
            FullName = $"{person.Name} {person.Surname}"  
        }).ToListAsync();
    }

}