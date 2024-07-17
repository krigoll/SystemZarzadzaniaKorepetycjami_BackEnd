using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class Test
{
    private Test()
    {
        TestForStudent = new HashSet<TestForStudent>();
        IdTask = new HashSet<Task>();
    }

    public int IdTest { get; private set; }
    public int IdTeacher { get; private set; }

            public virtual Teacher IdTeacherNavigation { get; private set; }
            public virtual ICollection<TestForStudent> TestForStudent { get; private set; }
            public virtual ICollection<Task> IdTask { get; private set; }
}
}
