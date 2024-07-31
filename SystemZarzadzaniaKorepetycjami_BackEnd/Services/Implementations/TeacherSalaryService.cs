using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class TeacherSalaryService : ITeacherSalaryService
{
    private readonly IPersonRepository _personRepository;
    private readonly ISubjectLevelRepository _subjectLevelRepository;
    private readonly ITeacherSalaryRepository _teacherSalaryRepository;

    public TeacherSalaryService(IPersonRepository personRepository, ITeacherSalaryRepository teacherSalaryRepository,
        ISubjectLevelRepository subjectLevelRepository)
    {
        _personRepository = personRepository;
        _teacherSalaryRepository = teacherSalaryRepository;
        _subjectLevelRepository = subjectLevelRepository;
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
                var subjectLevelId =
                    await _subjectLevelRepository.GetSubjectLevelIdBySubjectCategoryNameAndSubjectNameAsync(
                        dto.Subject_LevelName,
                        dto.Subject_Category_Name,
                        dto.SubjectName
                    );
                teacherSalaryList.Add(new TeacherSalary(dto.HourlyRate, person.IdPerson, subjectLevelId));
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