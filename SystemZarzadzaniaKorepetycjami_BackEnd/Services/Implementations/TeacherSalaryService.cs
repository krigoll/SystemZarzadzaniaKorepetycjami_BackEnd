using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class TeacherSalaryService : ITeacherSalaryService
{
    private readonly IPersonRepository _personRepository;
    private readonly ITeacherSalaryRepository _teacherSalaryRepository;

    public TeacherSalaryService(IPersonRepository personRepository, ITeacherSalaryRepository teacherSalaryRepository)
    {
        _personRepository = personRepository;
        _teacherSalaryRepository = teacherSalaryRepository;
    }

    public async Task<TeacherSalaryStatus> setTeacherSalaryAsync(List<TeacherSalaryDTO> teacherSalaryDTO)
    {
        try
        {
            var person = await _personRepository.FindPersonByEmailAsync(teacherSalaryDTO.First().PersonEmail);
            if (person == null) return TeacherSalaryStatus.INVALID_TEACHER_SALARY;

            List<TeacherSalary> teacherSalaryList = new List<TeacherSalary>();
            foreach (var dto in teacherSalaryDTO)
            {
                teacherSalaryList.Add(new TeacherSalary(dto.HourlyRate, person.IdPerson, dto.Subject_LevelId));
            }

            await _teacherSalaryRepository.AddTeacherSalaryAsync(teacherSalaryList);
            return TeacherSalaryStatus.VALID_TEACHER_SALARY;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return TeacherSalaryStatus.INVALID_TEACHER_SALARY;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return TeacherSalaryStatus.DATEBASE_ERROR;
        }
    }
}