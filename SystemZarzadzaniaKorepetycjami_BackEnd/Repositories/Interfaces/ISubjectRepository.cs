namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface ISubjectRepository
{
    public Task<List<string>> GetAllFullSubjects();
}