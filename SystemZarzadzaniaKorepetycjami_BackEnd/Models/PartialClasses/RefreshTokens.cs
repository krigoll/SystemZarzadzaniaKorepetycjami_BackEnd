using Microsoft.IdentityModel.Tokens;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class RefreshTokens
{
    public RefreshTokens(int idPerson, string token, DateTime expiryDate, DateTime createdDate)
    {
        SetIdPerson(idPerson);
        SetToken(token);
        SetExpiryDate(expiryDate);
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
        ExpiryDate = expiryDate;
    }

    public void SetCreatedDate(DateTime createdDate)
    {
        CreatedDate = createdDate;
    }
}