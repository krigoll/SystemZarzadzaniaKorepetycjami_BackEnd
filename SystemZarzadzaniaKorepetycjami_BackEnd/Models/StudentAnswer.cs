using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class StudentAnswer
{
    public int IdStudentAnswer { get; private set; }
    public string Answer { get; private set; }
    public int IdTestForStudent { get; private set; }
    public int IdTask { get; private set; }
    public int IdMark { get; private set; }

            public virtual Mark IdMarkNavigation { get; private set; }
            public virtual Task IdTaskNavigation { get; private set; }
            public virtual TestForStudent IdTestForStudentNavigation { get; private set; }
}
}
