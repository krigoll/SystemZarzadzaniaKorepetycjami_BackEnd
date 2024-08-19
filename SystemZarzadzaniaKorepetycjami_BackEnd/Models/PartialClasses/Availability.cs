namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Availability
{
    public Availability(int idTeacher, int idDayOfTheWeek, TimeOnly startTime, TimeOnly endTime)
    {
        SetIdTeacher(idTeacher);
        SetIdDayOfTheWeek(idDayOfTheWeek);
        SetTime(startTime, endTime);
    }

    public void SetIdTeacher(int idTeacher)
    {
        if (idTeacher <= 0) throw new ArgumentException("IdTeacher must be greater than 0.");

        IdTeacher = idTeacher;
    }

    public void SetIdDayOfTheWeek(int idDayOfTheWeek)
    {
        if (idDayOfTheWeek < 1 || idDayOfTheWeek > 7)
            throw new ArgumentException("IdDayOfTheWeek must be between 1 (Monday) and 7 (Sunday).");

        IdDayOfTheWeek = idDayOfTheWeek;
    }

    public void SetTime(TimeOnly startTime, TimeOnly endTime)
    {
        if (endTime < startTime) throw new ArgumentException("StartTime must be earlier than EndTime.");

        StartTime = startTime;
        EndTime = endTime;
    }
}