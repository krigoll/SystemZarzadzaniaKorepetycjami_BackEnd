using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
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

    public async Task<List<AvailabilityDTO>> GetTeacherAvailabilityByEmailAsync(string email)
    {
        var teacher = await _teacherRepository.GetTeacherByEmailAsync(email);
        if (teacher == null) return null;

        var availabilities = await _availabilityRepository.GetTeacherAvailabilityByTeacherAsync(teacher);
        var newAvailabilitiesToReturn = new List<AvailabilityDTO>();
        for (var i = 0; i < 7; i++)
        {
            var result = availabilities.FirstOrDefault(a => a.IdDayOfTheWeek == i + 1);
            if (result != null)
                newAvailabilitiesToReturn.Add(new AvailabilityDTO
                {
                    IdDayOfTheWeek = i + 1,
                    StartTime = result.StartTime.ToString(),
                    EndTime = result.EndTime.ToString()
                });
            else
                newAvailabilitiesToReturn.Add(new AvailabilityDTO
                {
                    IdDayOfTheWeek = i + 1,
                    StartTime = null,
                    EndTime = null,
                });
        }

        return newAvailabilitiesToReturn;
    }

    public async Task<SetAvailabilityStatus> CreateAndUpdateAvailabilityByEmail(string email,
        List<AvailabilityDTO> availabilities)
    {
        var teacher = await _teacherRepository.GetTeacherByEmailAsync(email);
        if (teacher == null) return SetAvailabilityStatus.INVALID_EMAIL;

        try
        {
            await _availabilityRepository.CreateAndUpdateAvailabilitiesByTeacher(teacher, availabilities);
            return SetAvailabilityStatus.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return SetAvailabilityStatus.INVALID_TIME;
        }
        catch (Exception e)
        {
            return SetAvailabilityStatus.DATABASE_ERROR;
        }
    }
}