namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class StudentAnswerDTO
{
    public int IdStudentAnswer { get; set; }
    public string Answer { get; set; }
    public int IdAssignment { get; set; }
}