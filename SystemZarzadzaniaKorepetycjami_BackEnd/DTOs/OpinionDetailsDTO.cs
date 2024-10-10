namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class OpinionDetailsDTO
{
    public int OpinionId { get; set; }
    public string StudentName { get; set; }
    public string TeacherName { get; set; }
    public int TeacherId { get; set; }
    public int Rating { get; set; }
    public string Content { get; set; }
}