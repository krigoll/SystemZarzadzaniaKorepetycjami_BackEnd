namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class AvailabilityDTO
{
    public int IdDayOfTheWeek { get; set; }
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? EndTime { get; set; }
}