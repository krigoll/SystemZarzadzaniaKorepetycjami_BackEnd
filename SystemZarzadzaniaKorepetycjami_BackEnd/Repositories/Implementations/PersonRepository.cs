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

        var results = await query.Select(person => new PersonDTO
        {
            IdPerson = person.IdPerson,
            FullName = $"{person.Name} {person.Surname}"
        }).ToListAsync();
        var sortedResults = results.OrderBy(p => p.FullName).ToList();

        return sortedResults;
    }

    public async Task<List<PersonInfoDTO>> GetAllPersonsAsync()
    {
        var persons = await (
            from person in _context.Person
            join teacher in _context.Teacher on person.IdPerson equals teacher.IdTeacher into teachers
            from teacher in teachers.DefaultIfEmpty()
            join student in _context.Student on person.IdPerson equals student.IdStudent into students
            from student in students.DefaultIfEmpty()
            where person.IdPerson == student.IdStudent || person.IdPerson == teacher.IdTeacher
            select new PersonInfoDTO
            {
                IdPerson = person.IdPerson,
                Name = person.Name,
                Surname = person.Surname,
                Image = person.Image == null ? null : Convert.ToBase64String(person.Image),
                IsStudent = student.IdStudent != null,
                IsTeacher = teacher.IdTeacher != null
            }
        ).ToListAsync();
        var sortedPersons = persons.OrderBy(p => p.Name).ThenBy(p => p.Surname).ToList();

        return sortedPersons;
    }
}