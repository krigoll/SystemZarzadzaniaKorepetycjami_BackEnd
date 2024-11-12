namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces
{
    public interface ITeacherService
    {
        public Task<List<TeacherDTO>> GetTeachersBySubjectCategoryAsync(int subjectLevelId, string email);
        public Task<List<TeacherDTO>> GetAllTeachersThatTeachStudentByStudentEmail(string studentEmail);
    }
}