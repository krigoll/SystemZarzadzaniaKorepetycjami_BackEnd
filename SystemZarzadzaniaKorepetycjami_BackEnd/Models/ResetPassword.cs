namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public partial class ResetPassword
    {
        public int IdResetPassword { get; private set; }
        public int IdPerson { get; private set; }
        public string Code { get; private set; }
        public DateTime ExpireDate { get; private set; }

        public virtual Person IdPersonNavigation { get; private set; }
    }
}