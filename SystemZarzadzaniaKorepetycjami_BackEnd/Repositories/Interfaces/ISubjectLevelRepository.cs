namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface ISubjectLevelRepository
{
    public Task<int> GetSubjectLevelIdBySubjectCategoryNameAndSubjectNameAsync(string subjectLevelName,
        string subjectCategoryName, string subjectName);
}