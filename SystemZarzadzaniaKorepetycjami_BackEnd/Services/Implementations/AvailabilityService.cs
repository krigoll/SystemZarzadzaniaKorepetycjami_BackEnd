using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class AvailabilityService : IAvailabilityService
{
    private readonly IAvailabilityRepository _availabilityRepository;
    private readonly ITeacherRepository _teacherRepository;

    public AvailabilityService(IAvailabilityRepository availabilityRepository, ITeacherRepository teacherRepository)
    {
        _availabilityRepository = availabilityRepository;
        _teacherRepository = teacherRepository;
    }

    public async Task<List<AvailabilityDTO>> GetTeacherAvailabilityByEamilAsync(string email)
    {
        var teacher = await _teacherRepository.GetTeacherByEmailAsync(email);
        if (teacher == null){
            return null;
        }
        var availabilitys = await _availabilityRepository.GetTeacherAvailabilityByTeacherAsync(teacher);
        var newAvailabilitysToReturn = new List<AvailabilityDTO>();
        for (var i = 0; i < availabilitys.Count; i++)
        {
            var result = availabilitys.FirstOrDefault(a => a.IdDayOfTheWeek == i+1);
            if (result != null)
                newAvailabilitysToReturn.Add(new AvailabilityDTO{
                    IdDayOfTheWeek = i+1,
                    StartTime = result.StartTime,
                    EndTime = result.EndTime,
                });
            else
                newAvailabilitysToReturn.Add(new AvailabilityDTO{
                    IdDayOfTheWeek = i+1,
                    StartTime = null,
                    EndTime = null,
                });
        }
        return newAvailabilitysToReturn;
    }
}