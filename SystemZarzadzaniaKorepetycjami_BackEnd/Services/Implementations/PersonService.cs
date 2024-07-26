using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<RegisterStarus> RegistrationPerson(RegistrationDTO registrationDto)
    {
        try
        {
            var newPerson = new Person(registrationDto.Name, registrationDto.Surname, registrationDto.BirthDate,
                registrationDto.Email, registrationDto.Password, registrationDto.PhoneNumber, registrationDto.Image);
            if (await _personRepository.AddPerson(newPerson)) return RegisterStarus.REGISTERED_USER;

            return RegisterStarus.DATEBASE_ERROR;
        }
        catch (ArgumentException e)
        {
            return RegisterStarus.INVALID_USER;
        }
        catch (Exception e)
        {
            return RegisterStarus.DATEBASE_ERROR;
        }
    }
}