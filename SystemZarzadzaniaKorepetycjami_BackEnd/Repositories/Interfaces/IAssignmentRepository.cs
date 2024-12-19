using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IAssignmentRepository
{
    public Task CreateAssignmentAsync(Assignment assignment);
    public Task<Assignment> GetAssignmentByIdAsync(int assignmentId);
    public Task DeleteAssignmentAsync(Assignment assignment);
    public Task<List<AssignmentDTO>> GetAssignmentsByTestIdAsync(int idTest);
}