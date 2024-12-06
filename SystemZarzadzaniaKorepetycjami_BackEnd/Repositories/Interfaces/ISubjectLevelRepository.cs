using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface ISubjectLevelRepository
{
    public Task<bool> IsSubjectLevelExistsBySubjectLevelIdAsync(int subjectLevelId);
    public Task CreateSubjectLevelAsync(SubjectLevel subjectLevel);
    public Task DeleteSubjectLevelAsync(int idSubjectCategory, string subjectLevelName);
    public Task<SubjectLevel> FindSubjectLevelByIdAsync(int idSubjectLevel);
}