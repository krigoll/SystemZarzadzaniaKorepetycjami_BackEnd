using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class TeacherSalary
{
    public int TeacherSalaryId { get; private set; }
    public decimal HourlyRate { get; private set; }
    public int IdTeacher { get; private set; }
    public int IdSubject { get; private set; }

            public virtual SubjectLevel IdSubjectNavigation { get; private set; }
            public virtual Teacher IdTeacherNavigation { get; private set; }
}
}
