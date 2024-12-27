namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class TestForStudentDTO
{
    public int IdTest { get; set; }
    public string Title { get; set; }
    public int NumberOfAssignments { get; set; }
    public string Fullname { get; set; }
    public DateTime CreationTime { get; set; }
}