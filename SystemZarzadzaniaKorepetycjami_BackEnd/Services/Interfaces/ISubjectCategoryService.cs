public interface ISubjectCategoryService
{
    public Task<SubjectCategoryStatus> CreateSubjectCategoryAsync(SubjectCategoryDTO subjectCategoryDTO);

    public Task<SubjectCategoryStatus> DeleteSubjectCategoryAsync(string subjectName,
        string subjectCategoryName);
}