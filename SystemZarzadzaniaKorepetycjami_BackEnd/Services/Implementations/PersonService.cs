using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly ILoginRepository _loginRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ITeacherRepository _teacherRepository;

    public PersonService(IPersonRepository personRepository, ILoginRepository loginRepository, IStudentRepository studentRepository, ITeacherRepository teacherRepository)
    {
        _personRepository = personRepository;
        _loginRepository = loginRepository;
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
    }

    public async Task<RegisterStarus> RegistrationPerson(RegistrationDTO registrationDto)
    {
        try
        {
            var isPersonExists = await _loginRepository.findPersonByEmailAsync(registrationDto.Email);
            if (isPersonExists != null)
            {
                return RegisterStarus.EMAIL_NOT_UNIQUE;
            }
            var newPerson = new Person(registrationDto.Name, registrationDto.Surname, registrationDto.BirthDate,
                registrationDto.Email, registrationDto.Password, registrationDto.PhoneNumber, registrationDto.Image);
            if (await _personRepository.AddPerson(newPerson))
            {
                if(registrationDto.IsStudent) 
                {
                    _studentRepository.AddStudent(new Student(newPerson.IdPerson));
                }
                    
                if(registrationDto.IsTeacher)
                {
                    _teacherRepository.AddTeacher(new Teacher(newPerson.IdPerson));
                }
                
                return RegisterStarus.REGISTERED_USER;
            }

            

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