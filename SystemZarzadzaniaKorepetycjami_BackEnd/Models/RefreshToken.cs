namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public partial class RefreshToken
    {
        public int IdJwt { get; private set; }
        public int IdPerson { get; private set; }
        public string Token { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public DateTime CreatedDate { get; private set; }

        public virtual Person IdPersonNavigation { get; private set; }
    }
}