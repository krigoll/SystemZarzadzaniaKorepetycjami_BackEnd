using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IAvailabilityRepository
{
    public Task<List<Availability>> GetTeacherAvailabilityByTeacherAsync(Teacher teacher);
}