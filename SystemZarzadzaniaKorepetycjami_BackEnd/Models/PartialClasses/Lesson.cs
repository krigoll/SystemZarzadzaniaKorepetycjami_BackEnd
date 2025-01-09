namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Lesson
{
    public Lesson(int? idStudent, int? idTeacher, int? idSubjectLevel, int idLessonStatus, DateTime startDate,
        int durationInMinutes)
    {
        SetIdStudent(idStudent);
        SetIdTeacher(idTeacher);
        SetIdSubjectLevel(idSubjectLevel);
        SetIdLessonStatus(idLessonStatus);
        SetStartDate(startDate);
        SetDurationInMinutes(durationInMinutes);
    }

    public void SetIdStudent(int? idStudent)
    {
        if (idStudent != null && idStudent <= 0)
            throw new ArgumentException("Invalid student ID");
        IdStudent = idStudent;
    }

    public void SetIdTeacher(int? idTeacher)
    {
        if (idTeacher != null && idTeacher <= 0)
            throw new ArgumentException("Invalid teacher ID");
        IdTeacher = idTeacher;
    }

    public void SetIdSubjectLevel(int? idSubjectLevel)
    {
        if (idSubjectLevel != null && idSubjectLevel <= 0)
            throw new ArgumentException("Invalid subject level ID");
        IdSubjectLevel = idSubjectLevel;
    }

    public void SetIdLessonStatus(int idLessonStatus)
    {
        if (idLessonStatus <= 0)
            throw new ArgumentException("Invalid lesson status ID");
        IdLessonStatus = idLessonStatus;
    }

    public void SetStartDate(DateTime startDate)
    {
        StartDate = startDate;
    }

    public void SetDurationInMinutes(int durationInMinutes)
    {
        if (durationInMinutes <= 0)
            throw new ArgumentException("Duration must be a positive number");
        DurationInMinutes = durationInMinutes;
    }
}