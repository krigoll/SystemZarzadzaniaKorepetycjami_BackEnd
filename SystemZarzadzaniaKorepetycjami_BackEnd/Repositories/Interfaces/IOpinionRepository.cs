using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IOpinionRepository
{
    public Task<List<OpinionDTO>> GetOpinionsByStudentAsync(Student student);
    public Task<List<OpinionDTO>> GetOpinionsByTeacherAsync(Teacher teacher);
    public Task<OpinionDetailsDTO> GetOpinionDetailsByIdAsync(int opinionId);
    public Task AddOpinionAsync(Opinion newOpinion);
    public Task<Opinion> GetOpinionByIdAsync(int opinionId);
    public Task UpdateOpinionAsync(Opinion opinion);
    public Task DeleteOpinionByIdAsync(Opinion opinion);
}