namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public partial class Assignment
    {
        private Assignment()
        {
            StudentAnswer = new HashSet<StudentAnswer>();
        }

        public int IdAssignment { get; }
        public string Content { get; private set; }
        public string Answer { get; private set; }
        public int IdTest { get; private set; }

        public virtual Test IdTestNavigation { get; }
        public virtual ICollection<StudentAnswer> StudentAnswer { get; private set; }
    }
}