namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public partial class Lesson
    {
        public int IdLesson { get; }
        public int? IdStudent { get; private set; }
        public int? IdTeacher { get; private set; }
        public int IdSubjectLevel { get; private set; }
        public int IdLessonStatus { get; private set; }
        public DateTime StartDate { get; private set; }
        public int DurationInMinutes { get; private set; }

        public virtual LessonStatus IdLessonStatusNavigation { get; }
        public virtual Student IdStudentNavigation { get; }
        public virtual SubjectLevel IdSubjectLevelNavigation { get; }
        public virtual Teacher IdTeacherNavigation { get; }
    }
}