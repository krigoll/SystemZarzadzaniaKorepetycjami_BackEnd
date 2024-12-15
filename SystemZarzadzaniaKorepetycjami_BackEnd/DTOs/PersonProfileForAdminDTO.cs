namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class PersonProfileForAdminDTO
{
    public int IdPerson { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string BirthDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public DateOnly JoiningDate { get; set; }
    public string Image { get; set; }
    public bool IsStudent { get; set; }
    public bool IsTeacher { get; set; }
    public bool IsBaned { get; set; }
    public int NumberOfDays { get; set; }
    public string Reason {get; set;}
}