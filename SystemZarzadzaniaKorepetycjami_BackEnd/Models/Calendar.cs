using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class Calendar
{
    public int IdTeacher { get; private set; }
    public DateTime StartingDate { get; private set; }
    public int NumberOfLessons { get; private set; }
    public int BreakTime { get; private set; }

            public virtual Teacher IdTeacherNavigation { get; private set; }
}
}
