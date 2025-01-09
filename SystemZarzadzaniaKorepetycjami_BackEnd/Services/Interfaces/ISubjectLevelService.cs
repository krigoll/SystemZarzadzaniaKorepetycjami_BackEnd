public interface ISubjectLevelService
{
    public Task<SubjectLevelStatus> CreateSubjectLevelAsync(SubjectLevelDTO subjectLevelDTO);

    public Task<SubjectLevelStatus> DeleteSubjectLevelAsync(string subjectName, string subjectCategoryName,
        string subjectLevelName);
}