using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class Opinion
{
    public int IdOpinion { get; private set; }
    public int Rating { get; private set; }
    public string Content { get; private set; }
    public int IdStudent { get; private set; }
    public int IdTeacher { get; private set; }

            public virtual Person IdStudentNavigation { get; private set; }
            public virtual Person IdTeacherNavigation { get; private set; }
}
}
