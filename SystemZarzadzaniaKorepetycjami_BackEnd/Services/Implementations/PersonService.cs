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

            if (!await _personRepository.IsPhoneNumberUniqueAsync(registrationDto.PhoneNumber))
                return RegisterStatus.PHONE_NUMBER_NOT_UNIQUE;

            var bytesImage = registrationDto.Image == null ? null : Convert.FromBase64String(registrationDto.Image);

            var newPerson = new Person(
                registrationDto.Name,
                registrationDto.Surname,
                registrationDto.BirthDate,
                registrationDto.Email,
                registrationDto.Password,
                registrationDto.PhoneNumber,
                bytesImage
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
            Console.WriteLine(e);
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
            isStudent = await _studentRepository.isStudentByEmail(email),
            isTeacher = await _teacherRepository.isTeacherByEmail(email)
        };
    }

    public async Task<PersonProfileDTO> GetPersonProfileByEmailAsync(string email)
    {
        var person = await _personRepository.FindPersonByEmailAsync(email);
        if (person == null) return null;

        var personRoles = await GetPersonRoleAsync(email);
        return new PersonProfileDTO
        {
            IdPerson = person.IdPerson,
            Name = person.Name,
            Surname = person.Surname,
            BirthDate = person.BirthDate.ToString(),
            Email = person.Email,
            PhoneNumber = person.PhoneNumber,
            JoiningDate = person.JoiningDate,
            Image = person.Image == null ? null : Convert.ToBase64String(person.Image),
            IsStudent = personRoles.isStudent,
            IsTeacher = personRoles.isTeacher,
            IsAdmin = personRoles.isAdmin
        };
    }

    public async Task<UpdateUserStatus> UpdateUserAsync(int idPerson, PersonEditProfileDTO personProfileDto)
    {
        try
        {
            var person = await _personRepository.FindPersonByIdAsync(idPerson);

            var isPersonExists = await _loginRepository.findPersonByEmailAsync(personProfileDto.Email);
            if (isPersonExists != null && personProfileDto.Email != person.Email)
                return UpdateUserStatus.EMAIL_NOT_UNIQUE;

            if (person.PhoneNumber != personProfileDto.PhoneNumber)
                if (!await _personRepository.IsPhoneNumberUniqueAsync(personProfileDto.PhoneNumber))
                    return UpdateUserStatus.PHONE_NUMBER_NOT_UNIQUE;

            var personRoles = await GetPersonRoleAsync(person.Email);
            var bytesImage = personProfileDto.Image == null ? null : Convert.FromBase64String(personProfileDto.Image);
            person.SetName(personProfileDto.Name);
            person.SetSurname(personProfileDto.Surname);
            person.SetEmail(personProfileDto.Email);
            person.SetPhoneNumber(personProfileDto.PhoneNumber);
            person.SetImage(bytesImage);


            if (!personProfileDto.IsStudent && personRoles.isStudent)
                await _studentRepository.RemoveStudentAsync(new Student(idPerson));

            if (personProfileDto.IsStudent && !personRoles.isStudent)
                await _studentRepository.AddStudent(new Student(idPerson));

            if (!personProfileDto.IsTeacher && personRoles.isTeacher)
                await _teacherRepository.RemoveTeacherAsync(new Teacher(idPerson));

            if (personProfileDto.IsTeacher && !personRoles.isTeacher)
                await _teacherRepository.AddTeacher(new Teacher(idPerson));

            await _personRepository.UpdateUserAsync(person);

            return UpdateUserStatus.UPDATED_USER;
        }
        catch (ArgumentException e)
        {
            return UpdateUserStatus.INVALID_USER;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return UpdateUserStatus.DATEBASE_ERROR;
        }
    }
}