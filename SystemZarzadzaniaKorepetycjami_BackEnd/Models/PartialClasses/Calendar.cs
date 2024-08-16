namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Calendar
{
    public Calendar(int idTeacher, DateTime startingDate, int numberOfLessons, int breakTime)
    {
        IdTeacher = idTeacher;
        StartingDate = startingDate;
        SetNumberOfLessons(numberOfLessons);
        SetBreakTime(breakTime);
    }

    public void SetNumberOfLessons(int numberOfLessons)
    {
        if (numberOfLessons < 0)
            throw new ArgumentException("Can not be on minus");
        this.NumberOfLessons = numberOfLessons;
    }

    public void SetBreakTime(int breakTime)
    {
        if (breakTime < 0)
            throw new ArgumentException("Can not be on minus");
        this.BreakTime = breakTime;
    }
}