namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs
{
    public class CalendarDTO
    {
        public int LessonId {get; set; }
        public string DateTime { get; set; }
        public string SubjectName { get; set; }
        public string StatusName { get; set; }
    }
}
