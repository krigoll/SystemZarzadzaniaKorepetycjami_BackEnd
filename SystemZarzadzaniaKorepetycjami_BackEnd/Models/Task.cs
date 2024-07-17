using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class Task
{
    private Task()
    {
        StudentAnswer = new HashSet<StudentAnswer>();
        IdTest = new HashSet<Test>();
    }

    public int IdTask { get; private set; }
    public string Content { get; private set; }
    public string Answer { get; private set; }
    public int IdSubjectLevel { get; private set; }
    public int IdTaskType { get; private set; }

            public virtual SubjectLevel IdSubjectLevelNavigation { get; private set; }
            public virtual TaskType IdTaskTypeNavigation { get; private set; }
            public virtual ICollection<StudentAnswer> StudentAnswer { get; private set; }
            public virtual ICollection<Test> IdTest { get; private set; }
}
}
