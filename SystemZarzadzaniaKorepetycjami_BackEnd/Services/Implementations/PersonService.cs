using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class PersonService : IPersonService
{
    private readonly ILoginRepository _loginRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ITeacherRepository _teacherRepository;

    public PersonService(IPersonRepository personRepository, ILoginRepository loginRepository,
        IStudentRepository studentRepository, ITeacherRepository teacherRepository)
    {
        _personRepository = personRepository;
        _loginRepository = loginRepository;
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
    }

    public async Task<RegisterStatus> RegistrationPerson(RegistrationDTO registrationDto)
    {
        try
        {
            var isPersonExists = await _loginRepository.findPersonByEmailAsync(registrationDto.Email);
            if (isPersonExists != null)
            {
                return RegisterStatus.EMAIL_NOT_UNIQUE;
            }

            var newPerson = new Person(
                registrationDto.Name,
                registrationDto.Surname,
                registrationDto.BirthDate,
                registrationDto.Email,
                registrationDto.Password,
                registrationDto.PhoneNumber,
                registrationDto.Image
            );

            var newPersonId = await _personRepository.AddPerson(newPerson);
            if (newPersonId <= 0)
            {
                return RegisterStatus.DATEBASE_ERROR;
            }

            if (registrationDto.IsStudent) await _studentRepository.AddStudent(new Student(newPersonId));

            if (registrationDto.IsTeacher) await _teacherRepository.AddTeacher(new Teacher(newPersonId));

            return RegisterStatus.REGISTERED_USER;
        }
        catch (ArgumentException e)
        {
            return RegisterStatus.INVALID_USER;
        }
        catch (Exception e)
        {
            return RegisterStatus.DATEBASE_ERROR;
        }
    }
}