using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

public interface IAvailabilityService
{
    public Task<List<AvailabilityDTO>> GetTeacherAvailabilityByEmailAsync(string email);

    public Task<SetAvailabilityStatus>
        CreateAndUpdateAvailabilityByEmail(string email, List<AvailabilityDTO> calendars);
}