using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface ISubjectLevelRepository
{
    public Task<SubjectLevel> GetSubjectLevelBySubjectCategoryNameAndSubjectNameAsync(String subjectLevelName, String subjectCategoryName, String subjectName);
}