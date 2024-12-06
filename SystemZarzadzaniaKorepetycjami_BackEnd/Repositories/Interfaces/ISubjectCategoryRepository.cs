using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public interface ISubjectCategoryRepository
{
    public Task CreateSubjectCategoryAsync(SubjectCategory subjectCategory);
    public Task DeleteSubjectCategoryAsync(int subjectId, string subjectCategoryName);
    public Task<SubjectCategory> FindSubjectCategoryByIdAsync(int idSubjectCategory);

    public Task<SubjectCategory> FindSubjectCategoryBySubjectIdAndSubjectCategoryNameAsync(int idSubject,
        string subjectCategoryName);
}