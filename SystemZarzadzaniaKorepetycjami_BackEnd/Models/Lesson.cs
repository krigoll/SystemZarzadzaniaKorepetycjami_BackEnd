namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public class Lesson
    {
        public int IdLesson { get; }
        public int IdStudent { get; }
        public int IdTeacher { get; }
        public int IdSubjectLevel { get; }
        public int IdLessonStatus { get; }
        public DateTime StartDate { get; }
        public int DurationInMinutes { get; }

        public virtual LessonStatus IdLessonStatusNavigation { get; }
        public virtual Student IdStudentNavigation { get; }
        public virtual SubjectLevel IdSubjectLevelNavigation { get; }
        public virtual Teacher IdTeacherNavigation { get; }
    }
}