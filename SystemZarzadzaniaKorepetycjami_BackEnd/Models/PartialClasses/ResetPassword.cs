namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class ResetPassword
{
    public ResetPassword(int idPerson, string code, DateTime expireDate)
    {
        if (idPerson < 0) throw new ArgumentException("Invalid Id Person");

        IdPerson = idPerson;

        if (code.Length > 6) throw new ArgumentException("Invalid Code");

        Code = code;

        ExpireDate = expireDate;
    }
}