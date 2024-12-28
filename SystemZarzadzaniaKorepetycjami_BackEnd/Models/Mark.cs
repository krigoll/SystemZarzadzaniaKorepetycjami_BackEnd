namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public class Mark
    {
        public int IdMark { get; }
        public string Description { get; }
        public bool Value { get; }
        public int? IdStudentAnswer { get; }

        public virtual StudentAnswer IdStudentAnswerNavigation { get; }
    }
}