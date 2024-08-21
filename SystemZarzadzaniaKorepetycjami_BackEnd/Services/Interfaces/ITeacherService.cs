using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces
{
    public interface ITeacherService
    {
        public Task<List<TeacherDTO>> GetTeachersBySubjectCategoryAsync(int subjectCategoryId);
    }
}