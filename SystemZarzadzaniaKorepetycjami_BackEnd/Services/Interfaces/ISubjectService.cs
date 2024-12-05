using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces
{
    public interface ISubjectService
    {
        public Task<List<SubjectDTO>> GetAllSubjectsAsync();
        public Task<List<SubjectTeacherDTO>> GetAllSubjectsEditAsync(string email);
        public Task<SubjectStatus> CreateSubjectAsync(string subjectName);
        public Task<SubjectStatus> DeleteSubjectAsync(string subjectName);
    }
}