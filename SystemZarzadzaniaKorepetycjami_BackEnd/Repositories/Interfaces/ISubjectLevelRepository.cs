using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface ISubjectLevelRepository
{
    public Task<SubjectLevelDto> GetSubjectLevelBySubjectCategoryNameAndSubjectNameAsync(string subjectLevelName,
        string subjectCategoryName, string subjectName);
}