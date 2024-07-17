namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public partial class LessonStatus
    {
        private LessonStatus()
        {
            Lesson = new HashSet<Lesson>();
        }

        public int IdLessonStatus { get; private set; }
        public string Status { get; private set; }

        public virtual ICollection<Lesson> Lesson { get; private set; }
    }
}