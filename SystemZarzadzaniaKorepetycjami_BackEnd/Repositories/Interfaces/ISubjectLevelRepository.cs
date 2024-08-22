using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface ISubjectLevelRepository
{
    public Task<bool> IsSubjectLevelExistsBySubjectLevelIdAsync(int subjectLevelId);
}