namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public partial class Test
    {
        private Test()
        {
            Assignment = new HashSet<Assignment>();
            TestForStudent = new HashSet<TestForStudent>();
        }

        public int IdTest { get; }
        public int IdTeacher { get; private set; }
        public string Title { get; private set; }

        public virtual Teacher IdTeacherNavigation { get; }
        public virtual ICollection<Assignment> Assignment { get; private set; }
        public virtual ICollection<TestForStudent> TestForStudent { get; private set; }
    }
}