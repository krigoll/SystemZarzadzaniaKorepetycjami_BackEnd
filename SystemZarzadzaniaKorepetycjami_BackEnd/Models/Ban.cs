namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public partial class Ban
    {
        public int IdBan { get; }
        public int IdPerson { get; private set; }
        public DateTime StartTime { get; private set; }
        public int LengthInDays { get; private set; }
        public string Reason { get; private set; }

        public virtual Person IdPersonNavigation { get; }
    }
}