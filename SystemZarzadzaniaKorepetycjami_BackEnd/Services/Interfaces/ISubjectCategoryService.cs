public interface ISubjectCategoryService
{
    public Task<SubjectCategoryStatus> CreateSubjectCategoryAsync(SubjectCategoryDTO subjectCategoryDTO);

    public Task<SubjectCategoryStatus> UpdateSubjectCategoryAsync(int idSubjectCategory,
        SubjectCategoryDTO subjectCategoryDTO);
}