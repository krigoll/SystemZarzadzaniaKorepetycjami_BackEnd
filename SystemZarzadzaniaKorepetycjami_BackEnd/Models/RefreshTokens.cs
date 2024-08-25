namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class RefreshTokens
{
    public int IdJwt { get; }
    public int IdPerson { get; private set; }
    public string Token { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public DateTime CreatedDate { get; private set; }

    public virtual Person IdPersonNavigation { get; }
}