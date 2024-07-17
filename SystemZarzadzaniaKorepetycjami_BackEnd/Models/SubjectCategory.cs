namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public partial class SubjectCategory
    {
        private SubjectCategory()
        {
            SubjectLevel = new HashSet<SubjectLevel>();
        }

        public int IdSubjectCategory { get; private set; }
        public string Name { get; private set; }
        public int IdSubject { get; private set; }

        public virtual Subject IdSubjectNavigation { get; private set; }
        public virtual ICollection<SubjectLevel> SubjectLevel { get; private set; }
    }
}