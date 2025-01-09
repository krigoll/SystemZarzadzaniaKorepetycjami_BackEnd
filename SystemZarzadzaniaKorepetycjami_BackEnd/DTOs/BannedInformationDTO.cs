namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class BannedInformationDTO
{ 
	public bool IsBaned{get; set;}
	public int NummberOfDays{get; set;}
    public string Reason {get; set;}
}