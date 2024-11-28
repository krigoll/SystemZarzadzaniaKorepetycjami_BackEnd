using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public interface ISubjectCategoryRepository
{
    public Task CreateSubjectCategoryAsync(SubjectCategory subjectCategory);
    public Task UpdateSubjectCategoryAsync(SubjectCategory subjectCategory);
    public Task<SubjectCategory> FindSubjectCategoryByIdAsync(int idSubjectCategory);
}