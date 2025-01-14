namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class LessonDatailsDTO
{
    public int IdLesson { get; set; }
    public string StartDate { get; set; }
    public int DurationInMinutes { get; set; }
    public string Status { get; set; }
    public string SubjectName { get; set; }
    public string TeacherName { get; set; }
    public int TeacherId { get; set; }
    public string StudentName { get; set; }
    public int Cost { get; set; }
}