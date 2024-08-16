namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Calendar
{
    public Calendar(int idTeacher, DateTime date, int numberOfLessons, int breakTime)
    {
        IdTeacher = idTeacher;
        StartingDate = date;
        NumberOfLessons = numberOfLessons;
        BreakTime = breakTime;
    }

    public void setNumberOfLessons(int numberOfLessons)
    {
        if (numberOfLessons < 0)
            throw new ArgumentException();
        this.NumberOfLessons = numberOfLessons;
    }

    public void setBreakTime(int breakTime)
    {
        if (breakTime < 0)
            throw new ArgumentException();
        this.BreakTime = breakTime;
    }
}