namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class RessetPassword
{
    public RessetPassword(int idPerson, string code, DateTime expiryDate)
    {
        if (idPerson < 0) throw new ArgumentException("Invalid Id Person");

        IdPerson = idPerson;

        if (code.Length > 6) throw new ArgumentException("Invalid Code");

        Code = code;

        ExpiryDate = expiryDate;
    }
}