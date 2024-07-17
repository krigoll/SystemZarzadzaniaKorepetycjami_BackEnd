using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class Mark
{
    private Mark()
    {
        StudentAnswer = new HashSet<StudentAnswer>();
    }

    public int IdMark { get; private set; }
    public string Description { get; private set; }
    public bool Value { get; private set; }

            public virtual ICollection<StudentAnswer> StudentAnswer { get; private set; }
}
}
