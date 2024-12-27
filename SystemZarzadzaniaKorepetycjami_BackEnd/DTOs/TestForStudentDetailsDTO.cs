namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class TestForStudentDetailsDTO
{
    public int IdTestForStudent { get; set; }
    public string Title { get; set; }
    public List<StudentAnswerAndMarkDTO> Assignment { get; set; }
}