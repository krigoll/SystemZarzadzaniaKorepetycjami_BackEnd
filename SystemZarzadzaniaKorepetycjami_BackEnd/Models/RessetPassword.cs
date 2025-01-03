namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class RessetPassword
{
    public int IdRessetPassword { get; }
    public int IdPerson { get; private set; }
    public string Code { get; private set; }
    public DateTime ExpiryDate { get; private set; }

    public virtual Person IdPersonNavigation { get; }
}