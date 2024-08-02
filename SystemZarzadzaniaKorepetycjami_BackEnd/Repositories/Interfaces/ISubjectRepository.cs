using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface ISubjectRepository
{
    public Task<List<SubjectDTO>> GetAllFullSubjects();
}