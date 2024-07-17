using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class Subject
{
    private Subject()
    {
        SubjectCategory = new HashSet<SubjectCategory>();
    }

    public int IdSubject { get; private set; }
    public string Name { get; private set; }

            public virtual ICollection<SubjectCategory> SubjectCategory { get; private set; }
}
}
