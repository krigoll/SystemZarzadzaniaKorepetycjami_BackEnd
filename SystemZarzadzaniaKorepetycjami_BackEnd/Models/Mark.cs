namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public partial class Mark
    {
        public int IdMark { get; }
        public string Description { get; private set; }
        public bool Value { get; private set; }
        public int IdStudentAnswer { get; private set; }

        public virtual StudentAnswer IdStudentAnswerNavigation { get; }
    }
}