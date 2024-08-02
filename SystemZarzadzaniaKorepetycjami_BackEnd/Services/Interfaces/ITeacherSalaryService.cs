using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces
{
    public interface ITeacherSalaryService
    {
        public Task<TeacherSalaryStatus> setTeacherSalaryAsync(List<TeacherSalaryDTO> teacherSalaryDTO);
    }
}