namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces
{
    public interface ISubjectService
    {
        public Task<List<String>> getAllSubjects();
    }
}
