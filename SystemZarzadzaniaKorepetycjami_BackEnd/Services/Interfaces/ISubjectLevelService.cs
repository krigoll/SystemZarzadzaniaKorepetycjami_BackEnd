public interface ISubjectLevelService
{
    public Task<SubjectLevelStatus> CreateSubjectLevelAsync(SubjectLevelDTO subjectLevelDTO);
    public Task<SubjectLevelStatus> UpdateSubjectLevelAsync(int idSubjectLevel, SubjectLevelDTO subjectLevelDTO);
}