namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class RegistrationDTO
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public byte[] Image { get; set; }
}