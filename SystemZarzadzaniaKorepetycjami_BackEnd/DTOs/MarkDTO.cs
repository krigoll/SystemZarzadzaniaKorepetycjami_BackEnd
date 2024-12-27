namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class MarkDTO
{
    public int IdMark { get; set; }
    public string Description { get; set; }
    public bool Value { get; set; }
    public int IdStudentAnswer { get; set; }
}