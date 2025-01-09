namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class StudentAnswerAndMarkDTO
{
    public int IdStudentAnswer { get; set; }
    public string StudentAnswer { get; set; }
    public int IdAssignment { get; set; }
    public string AnswerAssignment { get; set; }
    public string Content { get; set; }
    public int IdMark { get; set; }
    public string Description { get; set; }
    public bool Value { get; set; }
}