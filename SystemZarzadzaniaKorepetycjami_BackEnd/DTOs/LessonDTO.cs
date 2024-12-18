namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class LessonDTO
{
    public int LessonId { get; set; }
    public string DateTime { get; set; }
    public string SubjectCategoryName { get; set; }
    public string SubjectLevelName { get; set; }
    public string StudentName { get; set; }
    public string StudentSurname { get; set; }
    public int DurationInMinutes { get; set; }
}