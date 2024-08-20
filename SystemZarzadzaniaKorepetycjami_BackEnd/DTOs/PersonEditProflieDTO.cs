namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class PersonEditProfileDTO
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Image { get; set; }
    public bool IsStudent { get; set; }
    public bool IsTeacher { get; set; }
}