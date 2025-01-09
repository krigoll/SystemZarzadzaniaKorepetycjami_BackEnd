using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class Lesson
{
    public int IdLesson { get; private set; }
    public int? IdStudent { get; private set; }
    public int? IdTeacher { get; private set; }
    public int? IdSubjectLevel { get; private set; }
    public int IdLessonStatus { get; private set; }
    public DateTime StartDate { get; private set; }
    public int DurationInMinutes { get; private set; }

            public virtual LessonStatus IdLessonStatusNavigation { get; private set; }
            public virtual Student IdStudentNavigation { get; private set; }
            public virtual SubjectLevel IdSubjectLevelNavigation { get; private set; }
            public virtual Teacher IdTeacherNavigation { get; private set; }
}
}
