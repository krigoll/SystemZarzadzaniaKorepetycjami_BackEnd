namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public partial class StudentAnswer
    {
        private StudentAnswer()
        {
            Mark = new HashSet<Mark>();
        }

        public int IdStudentAnswer { get; }
        public string Answer { get; private set; }
        public int IdTestForStudent { get; private set; }
        public int IdAssignment { get; private set; }

        public virtual Assignment IdAssignmentNavigation { get; }
        public virtual TestForStudent IdTestForStudentNavigation { get; }
        public virtual ICollection<Mark> Mark { get; private set; }
    }
}