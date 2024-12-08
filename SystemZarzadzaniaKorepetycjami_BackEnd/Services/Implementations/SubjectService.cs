using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class SubjectService : ISubjectService
{
    private readonly IPersonRepository _personRepository;

    private readonly ISubjectRepository _subjectRepository;
    private readonly ITeacherRepository _teacherRepository;

    public SubjectService(ISubjectRepository subjectRepository, ITeacherRepository teacherRepository,
        IPersonRepository personRepository)
    {
        _subjectRepository = subjectRepository;
        _teacherRepository = teacherRepository;
        _personRepository = personRepository;
    }

    public async Task<List<SubjectDTO>> GetAllSubjectsAsync()
    {
        return await _subjectRepository.GetAllFullSubjects();
    }

    public async Task<List<SubjectDTO>> GetAllSubjectsAdminAsync()
    {
        return await _subjectRepository.GetAllSubjects();
    }

    public async Task<List<SubjectTeacherDTO>> GetAllSubjectsEditAsync(string email)
    {
        if (await _teacherRepository.isTeacherByEmail(email))
        {
            var person = await _personRepository.FindPersonByEmailAsync(email);
            return await _subjectRepository.GetAllFullSubjectsByTeacherId(person.IdPerson);
        }

        return null;
    }

    public async Task<SubjectStatus> CreateSubjectAsync(string subjectName)
    {
        try
        {
            Console.WriteLine("Jajko");
            Console.WriteLine(subjectName);
            Console.WriteLine("Jajko");
            var newSubject = new Subject(subjectName);

            await _subjectRepository.CreateSubjectAsync(newSubject);

            return SubjectStatus.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return SubjectStatus.INVALID_SUBJECT;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return SubjectStatus.SERVER_ERROR;
        }
    }

    public async Task<SubjectStatus> DeleteSubjectAsync(string subjectName)
    {
        try
        {
            await _subjectRepository.DeleteSubjectByNameAsync(subjectName);

            return SubjectStatus.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return SubjectStatus.INVALID_SUBJECT;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return SubjectStatus.SERVER_ERROR;
        }
    }
}