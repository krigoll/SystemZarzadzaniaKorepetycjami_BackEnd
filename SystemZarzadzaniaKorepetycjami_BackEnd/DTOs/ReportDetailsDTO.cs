namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class ReportDetailsDTO
{
    public int IdSender { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string DateTime { get; set; }
    public bool IsDealt { get; set; }
}