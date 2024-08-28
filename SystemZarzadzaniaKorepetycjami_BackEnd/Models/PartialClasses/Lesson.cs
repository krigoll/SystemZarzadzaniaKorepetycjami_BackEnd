using System.Globalization;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Lesson
{
    public Lesson(int idStudent, int idTeacher, int idSubjectLevel, int idLessonStatus, string startDay, string sartTime, int durationInMinutes)
        {
            SetIdStudent(idStudent);
            SetIdTeacher(idTeacher);
            SetIdSubjectLevel(idSubjectLevel);
            SetIdLessonStatus(idLessonStatus);
            SetStartDate(DateTime.ParseExact( $"{startDay} {sartTime}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture));
            SetDurationInMinutes(durationInMinutes);
        }

        public void SetIdStudent(int idStudent)
        {
            if (idStudent <= 0)
                throw new ArgumentException("Invalid student ID");
            IdStudent = idStudent;
        }

        public void SetIdTeacher(int idTeacher)
        {
            if (idTeacher <= 0)
                throw new ArgumentException("Invalid teacher ID");
            IdTeacher = idTeacher;
        }

        public void SetIdSubjectLevel(int idSubjectLevel)
        {
            if (idSubjectLevel <= 0)
                throw new ArgumentException("Invalid subject level ID");
            IdSubjectLevel = idSubjectLevel;
        }

        public void SetIdLessonStatus(int idLessonStatus)
        {
            if (idLessonStatus <= 0)
                throw new ArgumentException("Invalid lesson status ID");
            IdLessonStatus = idLessonStatus;
        }

        public void SetStartDate(DateTime startDate)
        {
            if (startDate < DateTime.Now)
                throw new ArgumentException("Start date cannot be in the past");
            StartDate = startDate;
        }

        public void SetDurationInMinutes(int durationInMinutes)
        {
            if (durationInMinutes <= 0)
                throw new ArgumentException("Duration must be a positive number");
            DurationInMinutes = durationInMinutes;
        }
}