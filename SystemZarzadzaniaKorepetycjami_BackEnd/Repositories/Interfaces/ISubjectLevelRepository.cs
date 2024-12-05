using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface ISubjectLevelRepository
{
    public Task<bool> IsSubjectLevelExistsBySubjectLevelIdAsync(int subjectLevelId);
    public Task CreateSubjectLevelAsync(SubjectLevel subjectLevel);
    public Task UpdateSubjectLevelAsync(SubjectLevel subjectLevel);
    public Task<SubjectLevel> FindSubjectLevelByIdAsync(int idSubjectLevel);
}