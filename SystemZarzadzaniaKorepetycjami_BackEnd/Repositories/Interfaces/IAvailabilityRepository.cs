using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IAvailabilityRepository
{
    public Task<List<Availability>> GetTeacherAvailabilityByTeacherAsync(Teacher teacher);
    public Task CreateAndUpdateAvailabilitiesByTeacher(Teacher teacher, List<AvailabilityDTO> availabilities);
    public Task<bool> IsThisLessonInAvailabilityTime(Lesson lesson);
}