using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class TestForStudent
{
    private TestForStudent()
    {
        StudentAnswer = new HashSet<StudentAnswer>();
    }

    public int IdTestForStudent { get; private set; }
    public int IdTest { get; private set; }
    public int IdStudent { get; private set; }
    public DateTime DateOfCreation { get; private set; }

            public virtual Student IdStudentNavigation { get; private set; }
            public virtual Test IdTestNavigation { get; private set; }
            public virtual ICollection<StudentAnswer> StudentAnswer { get; private set; }
}
}
