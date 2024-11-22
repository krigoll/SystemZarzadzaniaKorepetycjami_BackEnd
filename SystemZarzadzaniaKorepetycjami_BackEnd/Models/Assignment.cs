using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class Assignment
{
    private Assignment()
    {
        StudentAnswer = new HashSet<StudentAnswer>();
        IdTest = new HashSet<Test>();
    }

    public int IdAssignment { get; private set; }
    public string Content { get; private set; }
    public string Answer { get; private set; }

            public virtual ICollection<StudentAnswer> StudentAnswer { get; private set; }
            public virtual ICollection<Test> IdTest { get; private set; }
}
}
