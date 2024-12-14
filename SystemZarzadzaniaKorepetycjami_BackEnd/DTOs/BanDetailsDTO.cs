namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class BanDetailsDTO
{
	public int IdPerson{get; set;}
	public string BanedName{get; set;}
	public string StartTime{get; set;}
	public int LenghtInDays{get; set;}
	public string Reason{get; set;}
}