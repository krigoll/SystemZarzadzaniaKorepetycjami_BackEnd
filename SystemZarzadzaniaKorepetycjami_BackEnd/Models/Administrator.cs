namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public partial class Administrator
    {
        public int IdAdministrator { get; private set; }

        public virtual Person IdAdministratorNavigation { get; private set; }
    }
}