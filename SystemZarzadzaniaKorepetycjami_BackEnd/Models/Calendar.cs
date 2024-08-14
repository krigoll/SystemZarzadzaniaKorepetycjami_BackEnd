namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public class Calendar
    {
        public int IdTeacher { get; }
        public DateTime Date { get; }
        public int NumberOfLessons { get; }
        public int BreakTime { get; }

        public virtual Teacher IdTeacherNavigation { get; }
    }
}