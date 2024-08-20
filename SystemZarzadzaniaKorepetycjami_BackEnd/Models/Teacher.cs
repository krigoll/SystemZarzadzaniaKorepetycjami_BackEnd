using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class Teacher
{
    private Teacher()
    {
        Availability = new HashSet<Availability>();
        Lesson = new HashSet<Lesson>();
        Opinion = new HashSet<Opinion>();
        TeacherSalary = new HashSet<TeacherSalary>();
        Test = new HashSet<Test>();
    }

    public int IdTeacher { get; private set; }

            public virtual Person IdTeacherNavigation { get; private set; }
            public virtual ICollection<Availability> Availability { get; private set; }
            public virtual ICollection<Lesson> Lesson { get; private set; }
            public virtual ICollection<Opinion> Opinion { get; private set; }
            public virtual ICollection<TeacherSalary> TeacherSalary { get; private set; }
            public virtual ICollection<Test> Test { get; private set; }
}
}
