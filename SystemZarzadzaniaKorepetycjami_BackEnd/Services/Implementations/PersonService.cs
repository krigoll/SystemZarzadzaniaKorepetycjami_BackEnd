using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class PersonService : IPersonService
{
    private readonly IAdminRepository _adminRepository;
    private readonly ILoginRepository _loginRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ITeacherRepository _teacherRepository;

    public PersonService(IPersonRepository personRepository, ILoginRepository loginRepository,
        IStudentRepository studentRepository, ITeacherRepository teacherRepository, IAdminRepository adminRepository)
    {
        _personRepository = personRepository;
        _loginRepository = loginRepository;
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
        _adminRepository = adminRepository;
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

    public async Task<PersonRoleDTO> GetPersonRoleAsync(string email)
    {
        return new PersonRoleDTO
        {
            isAdmin = await _adminRepository.isAdministratorByEmail(email),
            isStudent = await _studentRepository.isPersonByEmail(email),
            isTeacher = await _teacherRepository.isTeacherByEmail(email)
        };
    }
}