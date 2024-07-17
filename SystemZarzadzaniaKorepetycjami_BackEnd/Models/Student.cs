using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class Student
{
    private Student()
    {
        Lesson = new HashSet<Lesson>();
        Opinion = new HashSet<Opinion>();
        TestForStudent = new HashSet<TestForStudent>();
    }

    public int IdStudent { get; private set; }

            public virtual Person IdStudentNavigation { get; private set; }
            public virtual ICollection<Lesson> Lesson { get; private set; }
            public virtual ICollection<Opinion> Opinion { get; private set; }
            public virtual ICollection<TestForStudent> TestForStudent { get; private set; }
}
}
