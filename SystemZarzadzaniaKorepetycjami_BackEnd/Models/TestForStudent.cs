namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public partial class TestForStudent
    {
        private TestForStudent()
        {
            StudentAnswer = new HashSet<StudentAnswer>();
        }

        public int IdTestForStudent { get; }
        public int IdTest { get; private set; }
        public int IdStudent { get; private set; }
        public DateTime DateOfCreation { get; private set; }
        public int IdTestForStudentStatus { get; private set; }

        public virtual Student IdStudentNavigation { get; }
        public virtual TestForStudentStatus IdTestForStudentStatusNavigation { get; }
        public virtual Test IdTestNavigation { get; }
        public virtual ICollection<StudentAnswer> StudentAnswer { get; private set; }
    }
}