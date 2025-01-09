namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class TestWithAssigmentDTO
{
    public int IdTest { get; set; }
    public string Title { get; set; }
    public List<AssignmentDTO> Assignments { get; set; }
}