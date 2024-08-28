namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class SingUpToLessonDTO
{
    public string StudentEmail {get; set;}
    public int TeacherId {get; set;}
    public int SubjectLevelId {get; set;}
    public string StartDate {get; set;}
    public string StartTime {get; set;}
    public int DurationInMinutes {get; set;}
    public string MessageContent {get; set;}
} 