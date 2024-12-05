using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface ISubjectRepository
{
    public Task<List<SubjectDTO>> GetAllFullSubjects();
    public Task<List<SubjectTeacherDTO>> GetAllFullSubjectsByTeacherId(int teacherId);
    public Task<Subject> FindSubjectByIdAsync(int idSubject);
    public Task CreateSubjectAsync(Subject subject);
    public Task DeleteSubjectByNameAsync(string subjectName);
}