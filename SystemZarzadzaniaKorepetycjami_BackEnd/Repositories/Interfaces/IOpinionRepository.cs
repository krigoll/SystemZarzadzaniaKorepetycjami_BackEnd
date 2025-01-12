using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

public interface IOpinionRepository
{
    public Task<List<OpinionDTO>> GetOpinionsByStudentAsync(int idStudent);
    public Task<List<OpinionDTO>> GetOpinionsByTeacherAsync(int idTeacher);
    public Task AddOpinionAsync(Opinion newOpinion);
    public Task<Opinion> GetOpinionByIdAsync(int opinionId);
    public Task<Opinion> GetOpinionByIdTeacherAndStudentIdAsync(int idTeacher, int idStudent);
    public Task UpdateOpinionAsync(Opinion opinion);
    public Task DeleteOpinionByIdAsync(Opinion opinion);
    public Task DeleteOpinionsByPersonId(int idPerson);
}