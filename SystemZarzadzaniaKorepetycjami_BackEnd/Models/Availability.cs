namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public partial class Availability
    {
        public int IdTeacher { get; private set; }
        public int IdDayOfTheWeek { get; private set; }
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }

        public virtual DayOfTheWeek IdDayOfTheWeekNavigation { get; }
        public virtual Teacher IdTeacherNavigation { get; }
    }
}