using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces
{
    public interface ISubjectService
    {
        public Task<List<SubjectDTO>> GetAllSubjectsAsync();
        public Task<List<SubjectTeacherDTO>> GetAllSubjectsEditAsync(string email);
    }
}
