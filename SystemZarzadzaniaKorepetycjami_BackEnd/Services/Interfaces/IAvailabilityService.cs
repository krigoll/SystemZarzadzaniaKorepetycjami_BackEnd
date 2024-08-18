using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

public interface IAvailabilityService
{
    public Task<List<AvailabilityDTO>> GetTeacherAvailabilityByEamilAsync(string email);
}