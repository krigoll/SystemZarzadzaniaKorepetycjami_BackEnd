using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class Calendar
{
    public int IdTeacher { get; private set; }
    public DateTime Date { get; private set; }
    public bool Availability { get; private set; }

            public virtual Teacher IdTeacherNavigation { get; private set; }
}
}
