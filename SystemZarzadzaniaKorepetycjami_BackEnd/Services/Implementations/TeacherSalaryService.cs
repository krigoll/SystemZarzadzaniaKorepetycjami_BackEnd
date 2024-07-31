using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class TeacherSalaryService : ITeacherSalaryService
{
    private readonly IPersonRepository _personRepository;
    private readonly ITeacherSalaryRepository _teacherSalaryRepository;
    private readonly ISubjectLevelRepository _subjectLevelRepository;

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
            List<TeacherSalary> teacherSalaryList = new List<TeacherSalary>();
            foreach (var dto in teacherSalaryDTO)
            {
                var subjectLevel = await _subjectLevelRepository.GetSubjectLevelBySubjectCategoryNameAndSubjectNameAsync(
                dto.Subject_LevelName,
                dto.Subject_Category_Name,
                dto.SubjectName
            );
                teacherSalaryList.Add(new TeacherSalary(dto.HourlyRate, person.Teacher.IdTeacher, subjectLevel.IdSubjectLevel));
            }
            await _teacherSalaryRepository.AddTeacherSalaryAsync(teacherSalaryList);
            return TeacherSalaryStatus.VALID_TEACHER_SALARY;
        }
        catch (ArgumentException e)
        {
            return TeacherSalaryStatus.INVALID_TEACHER_SALARY;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return TeacherSalaryStatus.DATEBASE_ERROR;
        }
    }

}