using Microsoft.IdentityModel.Tokens;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class RefreshToken
{
    public RefreshToken(int idPerson, string token, DateTime expireDate, DateTime createdDate)
    {
        SetIdPerson(idPerson);
        SetToken(token);
        SetExpiryDate(expireDate);
        SetCreatedDate(createdDate);
    }

    public void SetIdPerson(int idPerson)
    {
        if (idPerson < 0) throw new ArgumentException("Id can not be on minus");

        IdPerson = idPerson;
    }

    public void SetToken(string token)
    {
        if (token.IsNullOrEmpty()) throw new AggregateException("Token can not by empty");

        Token = token;
    }

    public void SetExpiryDate(DateTime expiryDate)
    {
        ExpireDate = expiryDate;
    }

    public void SetCreatedDate(DateTime createdDate)
    {
        CreatedDate = createdDate;
    }
}